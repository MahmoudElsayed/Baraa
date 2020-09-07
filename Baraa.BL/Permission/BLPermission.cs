using Baraa.BLL.ViewModel;
using Baraa.DAL.Contract;
using Baraa.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
 
namespace Baraa.BLL
{
    public class BLPermission : BaseEntity, IDisposable
    {
        IRepository<User> repoUser;
        IRepository<Permission> repoPermission;
        IRepository<PermissionControllerAction> repoPermissionControllerAction;
        IRepository<PermissionController> repoPermissionControllers;
        IRepository<CustomUserRole> repoUserRole;
        public BLPermission(IRepository<User> repoUser, IRepository<Permission> repoPermission
            , IRepository<PermissionControllerAction> repoPermissionControllerAction, IRepository<PermissionController> repoPermissionControllers
            , IRepository<CustomUserRole> repoUserRole)
        {
            this.repoUser = repoUser;
            this.repoPermission = repoPermission;
            this.repoPermissionControllerAction = repoPermissionControllerAction;
            this.repoPermissionControllers = repoPermissionControllers;
            this.repoUserRole = repoUserRole;
        }
        bool disposed = false;
        readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }

        ~BLPermission()
        {
            Dispose(false);
        }
        public IQueryable<UserRoleViewModel> GetAllUser()
        {
            var dbset = repoUser.DbSet;
            var data = repoUser.GetAll().Select(m => new UserRoleViewModel()
            {
                UserID =int.Parse( m.Id),
                RoleName = "",
                UserNameAR = m.UserName,
                UserNameEN = m.UserName,
            });
            //List<UserRoleViewModel> list = new List<UserRoleViewModel>();
            //foreach (var item in data)
            //{
            //    //var emp = Uow.Employee.GetAll(x => x.UserId == item.UserID).FirstOrDefault();
            //    var client = Uow.Client.GetAll(x => !x.IsDeleted && x.UserId == item.UserID).FirstOrDefault();
            //    var driver = Uow.Drivers.GetAll(x => !x.IsDeleted && x.UserId == item.UserID).FirstOrDefault();
            //    var operation = Uow.OperationUser.GetAll(x => !x.IsDeleted && x.UserId == item.UserID).FirstOrDefault();
            //    //if (emp != null)
            //    //{
            //    //    item.UserNameEN = emp.EmpName;
            //    //    list.Add(item);
            //    //}
            //    if (client != null)
            //    {
            //        item.UserNameEN = client.ClientNameEN;
            //        item.UserNameAR = client.ClientNameAR;
            //        list.Add(item);
            //    }
            //    if (driver != null)
            //    {
            //        item.UserNameEN = driver.DriversNameEN;
            //        item.UserNameAR = driver.DriversNameAR;
            //        list.Add(item);
            //    }
            //    if (operation != null)
            //    {
            //        item.UserNameEN = operation.OUserNameEn;
            //        item.UserNameAR = operation.OUserNameAr;
            //        list.Add(item);
            //    }

            //}
            return data;
        }

        public User GetUserById(int userID)
        {
            var user = repoUser.DbSet.FirstOrDefault(s => s.Id == userID+"");
            return user;
        }
        public bool GetPermissionByUserAndController(int userId, int permissionControllerID, int permissionActionID)
        {
            List<int> userRolesID = repoUserRole.GetAll(r => r.UserId == userId).Select(e => e.RoleId).ToList();
            return repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == permissionActionID &&
                         p.PermissionControllerActions.PermissionControllerID == permissionControllerID && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null;

        }
        public IQueryable<User> GetAllUsers()
        {
            return repoUser.GetAll();
        }
        public IQueryable<User> GetAllUsers(int userType)
        {
            return (GetAllUsers()).Where(query => query.UserType == userType);
        }
        public bool EditRolePermissions(PermissionViewModel model)
        {
            List<Permission> oldPermissions = repoPermission.DbSet.Where(s => s.RoleId == model.RoleID && s.UserId == model.UserId).ToList();
            string[] permissions = model.CheckedItems.Trim().Split(new string[] { "," }, StringSplitOptions.None);
            int temp;
            foreach (string p in permissions)
            {
                if (p.Contains("-"))
                {
                    temp = int.Parse(p.Substring(p.IndexOf("-") + 1)); // permissionControllerActionID
                    if (oldPermissions.FirstOrDefault(d => d.PermissionControllerActionID == temp) == null)
                    {
                        repoPermission.DbSet.Add(new Permission()
                        {
                            UserId = model.UserId,
                            PermissionControllerActionID = temp,
                            RoleId = model.RoleID,
                        });
                    }
                    else
                    {
                        oldPermissions.Remove(oldPermissions.FirstOrDefault(d => d.PermissionControllerActionID == temp));
                    }
                }
                else // this case Mean all actions in this controller has checked
                {
                    temp = int.Parse(p); // permissionControllerID
                    List<int> pcaIDs = repoPermissionControllerAction.DbSet.Where(c => c.PermissionControllerID == temp).Select(v => v.PermissionControllerActionID).ToList();
                    foreach (var item in pcaIDs)
                    {
                        if (oldPermissions.FirstOrDefault(d => d.PermissionControllerActionID == item) == null)
                        {
                            repoPermission.DbSet.Add(new Permission()
                            {
                                UserId = model.UserId,
                                PermissionControllerActionID = item,
                                RoleId = model.RoleID
                            });
                        }
                        else
                        {
                            oldPermissions.Remove(oldPermissions.FirstOrDefault(d => d.PermissionControllerActionID == item));
                        }
                    }
                }
            }
            if (oldPermissions.Count > 0)
            {
                foreach (var oldP in oldPermissions)
                {
                    repoPermission.Delete(oldP);
                }
            }
            return true;
        }

        public PermissionViewModel GetAllControllersByRoleAndBranch(int roleId, int userId)
        {
            PermissionViewModel result = new PermissionViewModel();
            result.Controllers = new List<PermissionControllerViewModel>();
            List<Permission> permissions = repoPermission.GetAll(p => p.UserId == userId && p.RoleId == roleId, "PermissionControllerActions").ToList();
            List<PermissionController> allControllers = repoPermissionControllers.GetAll(null, "PermissionControllerActions,PermissionControllerActions.PermissionActions").ToList();
            PermissionControllerViewModel pcvm;
            PermissionActionViewModel pavm;
            foreach (var c in allControllers)
            {
                pcvm = new PermissionControllerViewModel();
                pcvm.ID = c.PermissionControllerID.ToString();
                pcvm.Name = c.PermissionControllerNameAr;
                pcvm.Actions = new List<PermissionActionViewModel>();

                foreach (var ca in c.PermissionControllerActions)
                {
                    pavm = new PermissionActionViewModel();
                    pavm.ID = c.PermissionControllerID.ToString() + "-" + ca.PermissionControllerActionID.ToString();
                    pavm.Name = ca.PermissionActions.PermissionActionNameAr;
                    pavm.Allow = permissions.Where(x => x.PermissionControllerActionID == ca.PermissionControllerActionID).Any();
                    pcvm.Actions.Add(pavm);
                }

                result.Controllers.Add(pcvm);
            }

            return result;
        }

        public bool GetPermissionForAction(int controllerId, int actionId, string username)
        {
            List<int> allowedRolesID = repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == actionId &&
                                                                    p.PermissionControllerActions.PermissionControllerID == controllerId)
                                                                    .Select(r => int.Parse( r.Role.Id)).Distinct().ToList();

            int userId =int.Parse( repoUser.GetAll(s => s.UserName == username).Select(u => u.Id).FirstOrDefault());

            List<int> userRolesID = repoUserRole.GetAll(r => r.UserId == userId).Select(e => e.RoleId).ToList();

            foreach (int ur in userRolesID)
            {
                if (allowedRolesID.Contains(ur))
                {
                    return true;
                }
            }

            return false;
        }
        //public IQueryable<UserRoleViewModel> GetAllUsers()
        //{
        //    var data = repoUser.GetAll().Select(m => new UserRoleViewModel()
        //    {
        //        UserID = m.Id,
        //        RoleName = "",
        //    });
        //    List<UserRoleViewModel> list = new List<UserRoleViewModel>();
        //    foreach (var item in data)
        //    {
        //        var emp = Uow.Employee.GetAll(x => x.UserId == item.UserID).FirstOrDefault();
        //        var client = Uow.Client.GetAll(x => !x.IsDeleted && x.UserId == item.UserID).FirstOrDefault();
        //        var driver = Uow.Drivers.GetAll(x => !x.IsDeleted && x.UserId == item.UserID).FirstOrDefault();
        //        var operation = Uow.OperationUser.GetAll(x => !x.IsDeleted && x.UserId == item.UserID).FirstOrDefault();
        //        if (emp != null)
        //        {
        //            item.UserNameEN = emp.EmpName;
        //            list.Add(item);
        //        }
        //        if (client != null)
        //        {
        //            item.UserNameEN = client.ClientNameEN;
        //            list.Add(item);
        //        }
        //        if (driver != null)
        //        {
        //            item.UserNameEN = driver.DriversNameEN;
        //            list.Add(item);
        //        }
        //        if (operation != null)
        //        {
        //            item.UserNameEN = operation.OUserNameEn;
        //            list.Add(item);
        //        }

        //    }
        //    return list.AsQueryable();
        //}
        public UserPermission GetUserPermission(int controllerId, string username)
        {

            List<int> allowedRolesID_Insert = repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Insert &&
                                                                    p.PermissionControllerActions.PermissionControllerID == controllerId)
                                                                    .Select(r => int.Parse( r.Role.Id)).Distinct().ToList();

            List<int> allowedRolesID_Update = repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Update &&
                                                                   p.PermissionControllerActions.PermissionControllerID == controllerId)
                                                                   .Select(r => int.Parse( r.Role.Id)).Distinct().ToList();

            List<int> allowedRolesID_Delete = repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Delete &&
                                                                   p.PermissionControllerActions.PermissionControllerID == controllerId)
                                                                   .Select(r => int.Parse( r.Role.Id)).Distinct().ToList();

            //List<int> allowedRolesID_Approve = repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Approve &&
            //                                                    p.PermissionControllerActions.PermissionControllerID == controllerId)
            //                                                    .Select(r => r.Role.Id).Distinct().ToList();
            //List<int> allowedRolesID_Enable = repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Enable &&
            //                                                    p.PermissionControllerActions.PermissionControllerID == controllerId)
            //                                                    .Select(r => r.Role.Id).Distinct().ToList();
            //List<int> allowedRolesID_Disable = repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Disable &&
            //                                                    p.PermissionControllerActions.PermissionControllerID == controllerId)
            //                                                    .Select(r => r.Role.Id).Distinct().ToList();

            int userId =  int.Parse( repoUser.GetAll(s => s.UserName == username).Select(u => u.Id).FirstOrDefault());

            List<int> userRolesID = repoUserRole.GetAll(r => r.UserId == userId).Select(e => e.RoleId).ToList();

            UserPermission up = new UserPermission();

            foreach (int ur in userRolesID)
            {
                if (up.Insert && up.Update && up.Delete && up.Approve && up.Enable && up.Disable) { break; }
                //if (!up.View) { if (allowedRolesID_View.Contains(ur)) { up.View = true; } }
                if (!up.Insert) { if (allowedRolesID_Insert.Contains(ur)) { up.Insert = true; } }
                if (!up.Update) { if (allowedRolesID_Update.Contains(ur)) { up.Update = true; } }
                if (!up.Delete) { if (allowedRolesID_Delete.Contains(ur)) { up.Delete = true; } }
                //if (!up.Approve) { if (allowedRolesID_Approve.Contains(ur)) { up.Approve = true; } }
                //if (!up.Enable) { if (allowedRolesID_Enable.Contains(ur)) { up.Enable = true; } }
                //if (!up.Disable) { if (allowedRolesID_Disable.Contains(ur)) { up.Disable = true; } }
            }

            return up;
        }

        public int? GetUserPermissionBranch(string username, int controllerId, int actionId)
        {
            List<Permission> allowedViewPermission = repoPermission.GetAll(p => p.PermissionControllerActions.PermissionControllerID == controllerId && p.PermissionControllerActions.PermissionActionID == actionId, "PermissionControllerActions").ToList();

            int userId = int.Parse( repoUser.GetAll(s => s.UserName == username).Select(u => u.Id).FirstOrDefault());

            List<int> userRolesID = repoUserRole.GetAll(r => r.UserId == userId).Select(e => e.RoleId).ToList();

            List<int> branchs = allowedViewPermission.Where(s => userRolesID.Contains(s.RoleId)).Select(ss => ss.UserId).Distinct().ToList();

            return branchs.Count > 1 ? (int?)null : branchs[0];
        }

        public bool DeleteAllRolePermissions(int roleId)
        {
            repoPermission.DeleteRang(repoPermission.GetAll(s => s.RoleId == roleId));
            repoPermission.SaveChanges();
            return true;
        }

        public int GetUsersCountInRole(int roleId)
        {
            return repoUserRole.GetAll(s => s.RoleId == roleId).Count();
        }
        public int GetUsersCountInRoleByid(string UserName)
        {
            var username = repoUser.GetAll(e => e.UserName == UserName).FirstOrDefault();
            return repoUserRole.GetAll(s => s.UserId+"" == username.Id).FirstOrDefault().RoleId;
        }
        public List<int> GetuserRolesID(string UserName)
        {
            int userId =int.Parse( repoUser.GetAll(s => s.UserName == UserName).Select(u => u.Id).FirstOrDefault());
            List<int> userRolesID = repoUserRole.GetAll(r => r.UserId == userId).Select(e => e.RoleId).ToList();
            return userRolesID;
        }
        public PermissionViewModel GetAllControllersByRole(int roleId, int userId)
        {
            PermissionViewModel result = new PermissionViewModel();
            result.Controllers = new List<PermissionControllerViewModel>();
            List<Permission> permissions = repoPermission.GetAll(p => p.RoleId == roleId && p.UserId == userId, "PermissionControllerActions").ToList();
            List<PermissionController> allControllers = repoPermissionControllers.GetAll(null, "PermissionControllerActions,PermissionControllerActions.PermissionActions").OrderByDescending(x => x.PermissionControllerID).ToList();
            PermissionControllerViewModel pcvm;
            PermissionActionViewModel pavm;
            foreach (var c in allControllers)
            {
                pcvm = new PermissionControllerViewModel();
                pcvm.ID = c.PermissionControllerID.ToString();
                pcvm.Name = /*c.PermissionControllerNameAr + " | " +*/ c.PermissionControllerNameEn;
                //pcvm.Name = c.PermissionControllerNameAr;
                pcvm.Actions = new List<PermissionActionViewModel>();

                foreach (var ca in c.PermissionControllerActions)
                {
                    pavm = new PermissionActionViewModel();
                    pavm.ID = c.PermissionControllerID.ToString() + "-" + ca.PermissionControllerActionID.ToString();
                    pavm.Name = ca.PermissionActions.PermissionActionNameEn;
                    //pavm.Name = ca.PermissionAction.PermissionActionNameAr + " | " + ca.PermissionAction.PermissionActionNameEn;
                    pavm.Allow = permissions.Any(x => x.PermissionControllerActionID == ca.PermissionControllerActionID);
                    pcvm.Actions.Add(pavm);
                }

                result.Controllers.Add(pcvm);
            }

            return result;
        }
        public List<Permission> GetUserPermission(string username)
        {
            List<Permission> allowedViewPermission = null;


            if (!string.IsNullOrEmpty(username))
            {
                int userId =int.Parse( repoUser.GetAll(s => s.UserName == username).Select(u => u.Id).FirstOrDefault());

                List<int> userRolesID = repoUserRole.GetAll(r => r.UserId == userId).Select(e => e.RoleId).ToList();
                if (userRolesID.Count > 0)
                {
                    int viewActionID = (int)ActionEnum.View;
                    allowedViewPermission = repoPermission.GetAll(p =>
                   (
                   (p.PermissionControllerActions.PermissionActionID == viewActionID) ||
                   (p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Insert) ||
                   (p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Update) ||
                   (p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Delete)
                   )
                   , "PermissionControllerActions").Distinct().ToList();
                }
            }
            return allowedViewPermission;

        }
        public List<UserMenuItem> GetUserMenuItems(string username)
        {
            List<UserMenuItem> umiList = new List<UserMenuItem>();
 //           if (!string.IsNullOrEmpty(username))
 //           {
 //               int userId = repoUser.GetAll(s => s.UserName == username).Select(u => u.Id).FirstOrDefault();
 //               List<int> userRolesID = Uow.UserRoles.GetAll(r => r.UserId == userId).Select(e => e.RoleId).ToList();
 //               if (userRolesID.Count > 0)
 //               {
 //                   var viewActionID = (int)ActionEnum.View;
 //                   List<Permission> allowedViewPermission = repoPermission.GetAll(p =>
 //                   (
 //                   (p.PermissionControllerActions.PermissionActionID == viewActionID) ||
 //                   (p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Insert) ||
 //                   (p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Update) ||
 //                   (p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Delete)
 //                   )
 //                   , "PermissionControllerActions").Distinct().ToList();
 //                   #region منيو الموظفين
 //                   #region Home Menu Item
 //                   var UserData = new BLL.General.BLGeneral().GetUserInfo(username);
 //                   UserMenuItem umiHome = new UserMenuItem();
 //                   umiHome.Name = "Dashboard";
 //                   umiHome.Icon = "icon-home";

 //                   if (UserData.UserType == (int)TypeUserEnum.Client && (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Home &&
 //                    p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Clients && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null))
 //                   {
 //                       umiHome.Link = "/Client/Clients/SummeryClient";
 //                   }
 //                   else if (UserData.UserType == (int)TypeUserEnum.Operation)
 //                   {
 //                       umiHome.Link = "/Operation/OperationUser/Dashboard";
 //                   }

 //                   umiHome.SubItems = new List<UserMenuSubItem>();
 //                   if (umiHome.Link != null)
 //                   {
 //                       umiList.Add(umiHome);
 //                   }
 //                   //umiList.Add(umiHome);
 //                   #endregion
 //                   #region Manage DR
 //                   UserMenuItem umiManageDR = new UserMenuItem();
 //                   umiManageDR.Name = "ManageAWB";
 //                   umiManageDR.Icon = "icon-star";
 //                   umiManageDR.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Insert &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryRequest && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiManageDR.SubItems.Add(new UserMenuSubItem() { Name = "Add AWB", Link = "/DR/DeliveryRequest/add_One" });
 //                   }


 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Insert &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryRequest && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiManageDR.SubItems.Add(new UserMenuSubItem() { Name = "Import AWB", Link = "/DR/DeliveryRequest/ImportAWB_One" });
 //                   }

 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                  p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryRequest && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiManageDR.SubItems.Add(new UserMenuSubItem() { Name = "Bulk Update", Link = "/DR/DeliveryRequest/BulkUpdate" });
 //                   }

 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Insert &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryRequest && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiManageDR.SubItems.Add(new UserMenuSubItem() { Name = "Multi Shipment", Link = "/Home/TrackPackage_One" });
 //                   }

 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //               p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryRequest && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiManageDR.SubItems.Add(new UserMenuSubItem() { Name = "List Shipments", Link = "/DR/DeliveryRequest/Index_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //               p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryRequest && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiManageDR.SubItems.Add(new UserMenuSubItem() { Name = "Delivered Shipments", Link = "/DR/DeliveryRequest/DeliveredShipments" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //               p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryRequest && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiManageDR.SubItems.Add(new UserMenuSubItem() { Name = "Returned Shipments", Link = "/DR/DeliveryRequest/ReturnedShipments" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //              p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryRequest && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiManageDR.SubItems.Add(new UserMenuSubItem() { Name = "Rejected Shipments", Link = "/DR/DeliveryRequest/RejectedShipments" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //             p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryRequest && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiManageDR.SubItems.Add(new UserMenuSubItem() { Name = "Scheduling Shipments", Link = "/DR/DeliveryRequest/SchedulingShipments" });
 //                   }
 //                   //if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                   //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryRequest && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   //{
 //                   //    umiManageDR.SubItems.Add(new UserMenuSubItem() { Name = "Rollback AWB Delivered", Link = "/DR/DeliveryRequest/RollbackAWBDelivered_One" });
 //                   //}
 //                   if (umiManageDR.SubItems.Count > 0)
 //                   {
 //                       umiList.Add(umiManageDR);
 //                   }
 //                   #endregion
 //                   #region Delivery Order
 //                   UserMenuItem umiDeliveryOrder = new UserMenuItem();
 //                   umiDeliveryOrder.Name = "Delivery";
 //                   umiDeliveryOrder.Icon = "icon-star";
 //                   umiDeliveryOrder.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.NewAssign &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryOrder && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiDeliveryOrder.SubItems.Add(new UserMenuSubItem() { Name = "Assign Delivery", Link = "/DeliveryOrders/DeliveryOrder/AddDeliveryPackage_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Manage &&
 //                p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryOrder && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiDeliveryOrder.SubItems.Add(new UserMenuSubItem() { Name = "CheckOut DRS", Link = "/DeliveryOrders/DeliveryOrder/CheckoutOrder_One" });
 //                   }
 //                   //if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.NewAssign &&
 //                   //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryOrder && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   //{
 //                   //    umiDeliveryOrder.SubItems.Add(new UserMenuSubItem() { Name = "Assign With Select", Link = "/DeliveryOrders/DeliveryOrder/NewAssign_One" });
 //                   //}
 //                   // if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.NewAssign &&
 //                   //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryOrder && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   // {
 //                   //     umiDeliveryOrder.SubItems.Add(new UserMenuSubItem() { Name = "Assign Express", Link = "/DeliveryOrders/DeliveryOrder/AddExpressPackage_One" });
 //                   // }
 //                   //if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.NewAssign &&
 //                   //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryOrder && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   //{
 //                   //    umiDeliveryOrder.SubItems.Add(new UserMenuSubItem() { Name = "New Assign Express", Link = "/DeliveryOrders/DeliveryOrder/NewAssignExpress_One" });
 //                   //}
 //                   //if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.CancelAWB &&
 //                   //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryOrder && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   //{
 //                   //    umiDeliveryOrder.SubItems.Add(new UserMenuSubItem() { Name = "Cancel AWB", Link = "/DeliveryOrders/DeliveryOrder/CancelAWB_one" });
 //                   //}
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Manage &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryOrder && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiDeliveryOrder.SubItems.Add(new UserMenuSubItem() { Name = "Manage DRS", Link = "/DeliveryOrders/DeliveryOrder/ManageDeliveryOrder_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Confirmed &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryOrder && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiDeliveryOrder.SubItems.Add(new UserMenuSubItem() { Name = "Close DRS", Link = "/DeliveryOrders/DeliveryOrder/ConfirmedDelivery_One" });
 //                   }
 //                   // if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Confirmed &&
 //                   //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryOrder && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   // {
 //                   //     umiDeliveryOrder.SubItems.Add(new UserMenuSubItem() { Name = "Confirmed To Delivery", Link = "/DeliveryOrders/DeliveryOrder/ConfirmedToDelivery" });
 //                   // }
 //                   if (umiDeliveryOrder.SubItems.Count > 0)
 //                   {
 //                       umiList.Add(umiDeliveryOrder);
 //                   }
 //                   #endregion
 //                   #region Return Order
 //                   UserMenuItem umiReturnOrder = new UserMenuItem();
 //                   umiReturnOrder.Name = "Mainfest Mangment";
 //                   umiReturnOrder.Icon = "icon-star";
 //                   umiReturnOrder.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.NewAssign &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.ReturnOrder && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiReturnOrder.SubItems.Add(new UserMenuSubItem() { Name = "Assign Return", Link = "/DeliveryOrders/DeliveryOrder/AddReturnPackage_One" });
 //                   }
 //                   // if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.NewAssign &&
 //                   //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.ReturnOrder && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   // {
 //                   //     umiReturnOrder.SubItems.Add(new UserMenuSubItem() { Name = "Assign With Select", Link = "/DeliveryOrders/DeliveryOrder/NewAssignReturn_One" });
 //                   // }
 //                   // if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.CancelAWB &&
 //                   //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.ReturnOrder && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   // {
 //                   //     umiReturnOrder.SubItems.Add(new UserMenuSubItem() { Name = "Cancel AWB", Link = "/DeliveryOrders/DeliveryOrder/CancelAWBReturn_one" });
 //                   // }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Manage &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.ReturnOrder && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiReturnOrder.SubItems.Add(new UserMenuSubItem() { Name = "Manage Mainfest", Link = "/DeliveryOrders/DeliveryOrder/ManageReturnOrder_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Confirmed &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.ReturnOrder && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiReturnOrder.SubItems.Add(new UserMenuSubItem() { Name = "Close Mainfest", Link = "/DeliveryOrders/DeliveryOrder/ConfirmedReturn_One" });
 //                   }
 //                   // if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Confirmed &&
 //                   //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.ReturnOrder && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   // {
 //                   //     umiReturnOrder.SubItems.Add(new UserMenuSubItem() { Name = "Confirmed To Return", Link = "/DeliveryOrders/DeliveryOrder/ConfirmedToReturn" });
 //                   // }
 //                   if (umiReturnOrder.SubItems.Count > 0)
 //                   {
 //                       umiList.Add(umiReturnOrder);
 //                   }
 //                   #endregion
 //                   #region Courier AWB
 //                   UserMenuItem umiCourierAWB = new UserMenuItem();
 //                   umiCourierAWB.Name = "Courier AWB";
 //                   umiCourierAWB.Icon = "icon-star";
 //                   umiCourierAWB.SubItems = new List<UserMenuSubItem>();
 //                   //if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.AssignDelivery &&
 //                   //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.CourierAWB && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   //{
 //                   //    umiCourierAWB.SubItems.Add(new UserMenuSubItem() { Name = "Assign Courier", Link = "/DR/DeliveryRequest/CourierAssignDelivery_One" });
 //                   //}
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.NewAssign &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.CourierAWB && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiCourierAWB.SubItems.Add(new UserMenuSubItem() { Name = "Add order", Link = "/DeliveryOrders/DeliveryOrder/AddTransitPackage_One" });
 //                   }
 //                   // if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.NewAssign &&
 //                   //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.CourierAWB && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   // {
 //                   //     umiCourierAWB.SubItems.Add(new UserMenuSubItem() { Name = "Assign Transit", Link = "/DeliveryOrders/DeliveryOrder/CreateTransitCourier_One" });
 //                   // }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.CancelAWB &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.CourierAWB && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiCourierAWB.SubItems.Add(new UserMenuSubItem() { Name = "Manage Transit", Link = "/DeliveryOrders/DeliveryOrder/CancelAWBTransit_One" });
 //                   }
 //                   // if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.TransitHistory &&
 //                   //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.CourierAWB && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   // {
 //                   //     umiCourierAWB.SubItems.Add(new UserMenuSubItem() { Name = "Transit History", Link = "/DeliveryOrders/DeliveryOrder/TransitHistory" });
 //                   // }
 //                   //  if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.TrackAWBCourier &&
 //                   //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.CourierAWB && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   //  {
 //                   //      umiCourierAWB.SubItems.Add(new UserMenuSubItem() { Name = "Track AWB Courier", Link = "/DR/DeliveryRequest/TrackAWBCourier_One" });
 //                   //  }

 //                   if (umiCourierAWB.SubItems.Count > 0)
 //                   {
 //                       umiList.Add(umiCourierAWB);
 //                   }
 //                   #endregion
 //                   #region Customer Service
 //                   UserMenuItem umiBulkUpdate = new UserMenuItem();
 //                   umiBulkUpdate.Name = "Customer Service";
 //                   umiBulkUpdate.Icon = "icon-star";
 //                   umiBulkUpdate.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryRequest && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiBulkUpdate.SubItems.Add(new UserMenuSubItem() { Name = "Single Update", Link = "/DR/DeliveryRequest/PendingAWB_One" });
 //                   }
 //                   //  if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                   //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryRequest && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   //  {
 //                   //      umiBulkUpdate.SubItems.Add(new UserMenuSubItem() { Name = "Unknown Address", Link = "/DR/DeliveryRequest/UnknownLocations_One" });
 //                   //  }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryRequest && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiBulkUpdate.SubItems.Add(new UserMenuSubItem() { Name = "Bulk Update", Link = "/DR/DeliveryRequest/CustomerService" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //              p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryRequest && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiBulkUpdate.SubItems.Add(new UserMenuSubItem() { Name = "Rejected Shipments", Link = "/DR/DeliveryRequest/RejectedShipments" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //             p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryRequest && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiBulkUpdate.SubItems.Add(new UserMenuSubItem() { Name = "Scheduling Shipments", Link = "/DR/DeliveryRequest/SchedulingShipments" });
 //                   }
 //                   if (umiBulkUpdate.SubItems.Count > 0)
 //                   {
 //                       umiList.Add(umiBulkUpdate);
 //                   }
 //                   #endregion
 //                   #region Stock
 //                   UserMenuItem umiStock = new UserMenuItem();
 //                   umiStock.Name = "Stock Manage";
 //                   umiStock.Icon = "icon-star";
 //                   umiStock.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryRequest && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiStock.SubItems.Add(new UserMenuSubItem() { Name = "Stock Delivery", Link = "/DeliveryOrders/DeliveryOrder/StockDelivery_one" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryRequest && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiStock.SubItems.Add(new UserMenuSubItem() { Name = "Stock Return", Link = "/DeliveryOrders/DeliveryOrder/StockReturn_one" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                  p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryRequest && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiStock.SubItems.Add(new UserMenuSubItem() { Name = "Items Out Warehouse", Link = "/DR/DeliveryRequest/ItemsOutWarehouse_one" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryRequest && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiStock.SubItems.Add(new UserMenuSubItem() { Name = "Not Arrival", Link = "/DR/DeliveryRequest/NotArrival_one" });
 //                   }
 //                   if (umiStock.SubItems.Count > 0)
 //                   {
 //                       umiList.Add(umiStock);
 //                   }
 //                   #endregion
 //                   #region Employees
 //                   UserMenuItem umiOperationUser = new UserMenuItem();
 //                   umiOperationUser.Name = "Employees";
 //                   umiOperationUser.Icon = "icon-OperationUser";
 //                   umiOperationUser.SubItems = new List<UserMenuSubItem>();

 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                  p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.OperationUser && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiOperationUser.SubItems.Add(new UserMenuSubItem() { Name = "New Employee", Link = "/Operation/OperationUser/Create_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                  p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.OperationUser && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiOperationUser.SubItems.Add(new UserMenuSubItem() { Name = "Employee Mangment", Link = "/Operation/OperationUser/Index_One" });
 //                   }
 //                   if (umiOperationUser.SubItems.Count > 0)
 //                   {
 //                       umiList.Add(umiOperationUser);
 //                   }

 //                   #endregion
 //                   #region Clients
 //                   UserMenuItem umiClients = new UserMenuItem();
 //                   umiClients.Name = "Clients";
 //                   umiClients.Icon = "icon-Clients";
 //                   umiClients.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.ClientsAdmin && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiClients.SubItems.Add(new UserMenuSubItem() { Name = "New Customer", Link = "/Client/Clients/Add_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.ClientsAdmin && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiClients.SubItems.Add(new UserMenuSubItem() { Name = "Manage Customer", Link = "/Client/Clients/Index_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                  p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.ShippingCharges && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiClients.SubItems.Add(new UserMenuSubItem() { Name = "Shipping Charges", Link = "/Setting/ShippingCharges/AddShippingCharges" });
 //                   }
 //                   if (umiClients.SubItems.Count > 0)
 //                   {
 //                       umiList.Add(umiClients);
 //                   }
 //                   #endregion
 //                   #region Drivers Admin
 //                   UserMenuItem umiDrivers = new UserMenuItem();
 //                   umiDrivers.Name = "Drivers";
 //                   umiDrivers.Icon = "icon-Drivers";
 //                   umiDrivers.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DriversAdmin && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiDrivers.SubItems.Add(new UserMenuSubItem() { Name = "New Courier", Link = "/Driver/Drivers/Add_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                  p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DriversAdmin && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiDrivers.SubItems.Add(new UserMenuSubItem() { Name = "Manage Courier", Link = "/Driver/Drivers/Index_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.DriverRates &&
 //               p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Reports && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiDrivers.SubItems.Add(new UserMenuSubItem() { Name = "Rates", Link = "/DR/DeliveryRequest/DriverRates_One" });
 //                   }
 //                   if (umiDrivers.SubItems.Count > 0)
 //                   {
 //                       umiList.Add(umiDrivers);
 //                   }
 //                   #endregion
 //                   #region Brunch
 //                   UserMenuItem umiAgent = new UserMenuItem();
 //                   umiAgent.Name = "Agent";
 //                   umiAgent.Icon = "icon-star";
 //                   umiAgent.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //             p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Agent && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiAgent.SubItems.Add(new UserMenuSubItem() { Name = "New Branch", Link = "/Setting/Agent/Index" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //              p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Agent && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiAgent.SubItems.Add(new UserMenuSubItem() { Name = "Manage Branch", Link = "/Setting/Agent/ViewAgent" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.OperationAgent &&
 //                  p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Agents && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiAgent.SubItems.Add(new UserMenuSubItem() { Name = "Operation Agent", Link = "/Setting/OperationAgent/Index_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.AgentCity &&
 //                p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Agents && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiAgent.SubItems.Add(new UserMenuSubItem() { Name = "Agent City", Link = "/Setting/AgentCity/Index_One" });
 //                   }
 //                   if (umiAgent.SubItems.Count > 0)
 //                   {
 //                       umiList.Add(umiAgent);
 //                   }
 //                   #endregion
 //                   #region Setting
 //                   UserMenuItem umiSetting = new UserMenuItem();
 //                   umiSetting.Name = "Setting";
 //                   umiSetting.Icon = "icon-star";
 //                   umiSetting.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                  p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Countries && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Status Mangment", Link = "/Setting/Agent/StatusIndex" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Countries && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Countries", Link = "/Setting/Countries/Index" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Cities && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Cities", Link = "/Setting/Cities/Index" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Blocks && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Blocks", Link = "/Setting/Blocks/Index" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Banks && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Banks", Link = "/Setting/Banks/Index" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Nationality && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Nationality", Link = "/Setting/Nationality/Index" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                  p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Positions && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Positions", Link = "/Setting/Positions/Index" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.SecurityQuestion && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Security Question", Link = "/Setting/SecurityQuestion/Index" });
 //                   }

 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.VehicleType && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Vehicle Type", Link = "/Setting/VehicleType/Index" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.VehicleYears && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Vehicle Years", Link = "/Setting/VehicleYears/Index" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.VehicleModelTypes && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Vehicle Model Types", Link = "/Setting/VehicleModelTypes/Index" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.VehicleManufacturers && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Vehicle Manufacturers", Link = "/Setting/VehicleManufacturers/Index" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.UnDeliveredReason && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Un Delivered Reason", Link = "/Setting/UnDeliveredReason/Index" });
 //                   }
 //                //   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.RequestTaxs && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                //   {
 //                //       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Request Taxs", Link = "/Setting/RequestTaxs/Index" });
 //                //   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.ClientsEmails && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Clients Emails", Link = "/Setting/ClientsEmails/Index" });
 //                   }
 //                //   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.WeightVolume && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                //   {
 //                //       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Extra Weight Volume", Link = "/Setting/ExtraWeightVolume/Index" });
 //                //   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.CourierCompany && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Courier Company", Link = "/Setting/CourierCompany/Index" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.EvaluationQuestions && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Evaluation Questions", Link = "/Setting/EvaluationQuestions/Index" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Zone && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Zone", Link = "/Setting/Zone/Index" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Basket && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Basket", Link = "/Setting/Basket/Index" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Shelve && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Shelve", Link = "/Setting/Shelve/Index" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //               p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DriverToClient && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Driver Pickers", Link = "/Driver/DriverToClient/Index_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.ShipmentTypeDriver &&
 //                  p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DriversAdmin && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Drivers Regions", Link = "/Driver/Drivers/ShipmentTypeDriver_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.DeliveryCost && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Delivery Cost", Link = "/Driver/DeliveryCost/Index_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.NewOpenBalance &&
 //               p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.FinanceClient && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "New Open Balance", Link = "/Financial/Invoice/NewOpenBalance_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.NewOpenBalance &&
 //             p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.FinanceClient && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSetting.SubItems.Add(new UserMenuSubItem() { Name = "Vat Discount", Link = "/Setting/VatDiscount/Index" });
 //                   }
 //                   if (umiSetting.SubItems.Count > 0)
 //                   {
 //                       umiList.Add(umiSetting);
 //                   }
 //                   #endregion
 //                   #region Permissions
 //                   UserMenuItem umiPermissions = new UserMenuItem();
 //                   umiPermissions.Name = "Permissions";
 //                   umiPermissions.Icon = "icon-star";
 //                   umiPermissions.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Insert &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Permission && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiPermissions.SubItems.Add(new UserMenuSubItem() { Name = "Role Permissions", Link = "/Permission/Permissions/RolePermissions_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Insert &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Permission && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiPermissions.SubItems.Add(new UserMenuSubItem() { Name = "Users In Role", Link = "/Permission/Permissions/UsersInRole_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Role && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiPermissions.SubItems.Add(new UserMenuSubItem() { Name = "Role", Link = "/Permission/Role/Index_One" });
 //                   }
 //                   if (umiPermissions.SubItems.Count > 0)
 //                   {
 //                       umiList.Add(umiPermissions);
 //                   }
 //                   #endregion
 //                   #region FinanceClient
 //                   UserMenuItem umiFinanceClient = new UserMenuItem();
 //                   umiFinanceClient.Name = "Finance Client";
 //                   umiFinanceClient.Icon = "icon-star";
 //                   umiFinanceClient.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.GenerateInvoice &&
 //                  p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.FinanceClient && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiFinanceClient.SubItems.Add(new UserMenuSubItem() { Name = "New Charge Invoice", Link = "/Financial/Invoice/GenerateInvoice_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.NewPayment &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.FinanceClient && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiFinanceClient.SubItems.Add(new UserMenuSubItem() { Name = "New COD Invoice", Link = "/Financial/Invoice/NewPayment_One" });
 //                   }
 //                 //  if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.NewCollection &&
 //                 //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.FinanceClient && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                 //  {
 //                 //      umiFinanceClient.SubItems.Add(new UserMenuSubItem() { Name = "New COD Payment", Link = "/Financial/Invoice/NewCollection_One" });
 //                 //  }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.NewCollection &&
 //               p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.FinanceClient && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiFinanceClient.SubItems.Add(new UserMenuSubItem() { Name = "New Charge Collection", Link = "/Financial/Invoice/NewChargeCollection" });
 //                   }
 //                   if (repoPermission.GetAll(p => (p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.ApproveAdmin || p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.ApproveOperation) &&
 //               p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.FinanceClient && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiFinanceClient.SubItems.Add(new UserMenuSubItem() { Name = "Transfer Approval", Link = "/Financial/Invoice/TransferApproval" });
 //                   }
 //                   if (repoPermission.GetAll(p => (p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.ApproveAdmin || p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.ApproveOperation) &&
 //              p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.FinanceClient && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiFinanceClient.SubItems.Add(new UserMenuSubItem() { Name = "Pay To Customer", Link = "/Financial/Invoice/TransferApprovalPayment" });
 //                   }
 //                   if (umiFinanceClient.SubItems.Count > 0)
 //                   {
 //                       umiList.Add(umiFinanceClient);
 //                   }
 //                   UserMenuItem umiFinanceClientSec = new UserMenuItem();
 //                   umiFinanceClientSec.Name = "Financial Reports";
 //                   umiFinanceClientSec.Icon = "icon-star";
 //                   umiFinanceClientSec.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.ViewInvoice &&
 //                p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.FinanceClient && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiFinanceClientSec.SubItems.Add(new UserMenuSubItem() { Name = "Charge Actions", Link = "/Financial/Invoice/ViewInvoice_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.ReportPayOrder &&
 //                p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.FinanceClient && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiFinanceClientSec.SubItems.Add(new UserMenuSubItem() { Name = "COD Actions", Link = "/Financial/Invoice/ReportPayOrder_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.InvoicesGenerated &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.FinanceClient && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiFinanceClientSec.SubItems.Add(new UserMenuSubItem() { Name = "List Transfered", Link = "/Financial/Invoice/ListTransfered" });
 //                   }

 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.ReportPaidAWB &&
 //                p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.FinanceClient && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiFinanceClientSec.SubItems.Add(new UserMenuSubItem() { Name = "List Collected", Link = " /Financial/Invoice/ListCollected" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.ClientBalance &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.FinanceClient && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiFinanceClientSec.SubItems.Add(new UserMenuSubItem() { Name = "Client Balance", Link = "/Financial/Invoice/ClientBalance_One" });
 //                   }
 //                   if (umiFinanceClientSec.SubItems.Count > 0)
 //                   {
 //                       umiList.Add(umiFinanceClientSec);
 //                   }

 //                   #endregion
 //                   #region FinanceCaptain
 //                   UserMenuItem umiFinanceCaptain = new UserMenuItem();
 //                   umiFinanceCaptain.Name = "Finance Captain Mangemnt";
 //                   umiFinanceCaptain.Icon = "icon-star";
 //                   umiFinanceCaptain.SubItems = new List<UserMenuSubItem>();

 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.PrepareOrderPay &&
 //                p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.FinanceCaptain && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiFinanceCaptain.SubItems.Add(new UserMenuSubItem() { Name = "Prepare Order Pay", Link = "/Driver/Drivers/PrepareOrderPay_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.NewOrderPay &&
 //                p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.FinanceCaptain && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiFinanceCaptain.SubItems.Add(new UserMenuSubItem() { Name = "New Order Pay", Link = "/Driver/Drivers/NewOrderPay_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.ReportOrdersPaid &&
 //               p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.FinanceCaptain && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiFinanceCaptain.SubItems.Add(new UserMenuSubItem() { Name = "Report Orders Paid", Link = "/Driver/Drivers/ReportOrdersPaid_One" });
 //                   }
 //                   if (umiFinanceCaptain.SubItems.Count > 0)
 //                   {
 //                       umiList.Add(umiFinanceCaptain);
 //                   }
 //                   #endregion
 //                   #region Reports
 //                   #region Signatures
 //                   UserMenuItem umiSignatures = new UserMenuItem();
 //                   umiSignatures.Name = "Signatures";
 //                   umiSignatures.Icon = "icon-star";
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Pickup &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Signatures && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiSignatures.Link = "/DR/DeliveryRequest/PickupSignatures_One";
 //                   }
 //                   umiSignatures.SubItems = new List<UserMenuSubItem>();
 //                   if (umiSignatures.Link != null)
 //                   {
 //                       umiList.Add(umiSignatures);
 //                   }
 //                   //if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Pickup &&
 //                   //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Signatures && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   //{
 //                   //    umiSignatures.SubItems.Add(new UserMenuSubItem() { Name = "Pickup", Link = "/DR/DeliveryRequest/PickupSignatures_One" });
 //                   //}
 //                   //if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Return &&
 //                   //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Signatures && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   //{
 //                   //    umiSignatures.SubItems.Add(new UserMenuSubItem() { Name = "Return", Link = "/DR/DeliveryRequest/ReturnSignatures_One" });
 //                   //}
 //                   //if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Express &&
 //                   //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Signatures && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   //{
 //                   //    umiSignatures.SubItems.Add(new UserMenuSubItem() { Name = "Express", Link = "/DR/DeliveryRequest/ExpressSignatures_One" });
 //                   //}
 //                   // if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Transit &&
 //                   //p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Signatures && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   // {
 //                   //     umiSignatures.SubItems.Add(new UserMenuSubItem() { Name = "Transit", Link = "/DR/DeliveryRequest/TransitSignatures_One" });
 //                   // }
 //                   //if (umiSignatures.SubItems.Count > 0)
 //                   //{
 //                   //    umiList.Add(umiSignatures);
 //                   //}
 //                   #endregion

 //                   UserMenuItem umiReports = new UserMenuItem();
 //                   umiReports.Name = "Reports";
 //                   umiReports.Icon = "icon-star";
 //                   umiReports.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.DeliveryReports &&
 //                  p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Reports && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiReports.SubItems.Add(new UserMenuSubItem() { Name = "Delivery Reports", Link = "/DeliveryOrders/DeliveryOrder/DeliveryReports_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.ReturnOrderReport &&
 //                  p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Reports && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiReports.SubItems.Add(new UserMenuSubItem() { Name = "Return Report", Link = "/DeliveryOrders/DeliveryOrder/ReturnOrderReport_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.ClientsReports &&
 //                  p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Reports && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiReports.SubItems.Add(new UserMenuSubItem() { Name = "Clients Reports", Link = "/DR/DeliveryRequest/ClientsReports_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.DailyShipmentReport &&
 //                  p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Reports && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiReports.SubItems.Add(new UserMenuSubItem() { Name = "Daily Shipment Report", Link = "/DR/DeliveryRequest/DailyShipmentReport_One" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.TrackDriver &&
 //                  p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Reports && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiReports.SubItems.Add(new UserMenuSubItem() { Name = "Track Driver", Link = "/DR/DeliveryRequest/TrackDriver_One" });
 //                   }

 //                   if (umiReports.SubItems.Count > 0)
 //                   {
 //                       umiList.Add(umiReports);
 //                   }
 //                   #endregion

 //                   #region NPS
 //                   UserMenuItem umiNPS = new UserMenuItem();
 //                   umiNPS.Name = "NPS";
 //                   umiNPS.Icon = "icon-NPS";
 //                   umiNPS.Link = "#";

 //                   umiNPS.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Charts &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.NPS && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiNPS.SubItems.Add(new UserMenuSubItem() { Name = "Survey Charts", Link = "/Home/Evaluation" });
 //                   }
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Results &&
 //                  p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.NPS && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiNPS.SubItems.Add(new UserMenuSubItem() { Name = "Survey Results", Link = "/Home/EvaluationDetails" });
 //                   }

 //                   if (umiNPS.SubItems.Count > 0)
 //                   {
 //                       umiList.Add(umiNPS);
 //                   }
 //                   #endregion
 //                   #endregion
 //                   #region منيو العملاء
 //                   #region New Shipment
 //                   UserMenuItem umiCManageAWB = new UserMenuItem();
 //                   umiCManageAWB.Name = "Create Shipment";
 //                   umiCManageAWB.Icon = "icon-star";
 //                   umiCManageAWB.Link = "/Client/Clients/ClientAddAWB_One";

 //                   umiCManageAWB.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.NewAWB &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Clients && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiList.Add(umiCManageAWB);
 //                   }

 //                   #endregion
 //                   #region Bulk Excel Shipment
 //                   UserMenuItem umiCBulkExcelShipment = new UserMenuItem();
 //                   umiCBulkExcelShipment.Name = "Data Import";
 //                   umiCBulkExcelShipment.Icon = "icon-BulkExcelShipment";
 //                   umiCBulkExcelShipment.Link = "/Client/Clients/ClientImportAWB_One";
 //                   umiCBulkExcelShipment.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.NewAWB &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Clients && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiList.Add(umiCBulkExcelShipment);
 //                   }
 //                   #endregion
 //                   #region Manage Shipment
 //                   UserMenuItem umiCSearchAWB = new UserMenuItem();
 //                   umiCSearchAWB.Name = "Manage Shipment";
 //                   umiCSearchAWB.Icon = "icon-SearchAWB";
 //                   umiCSearchAWB.Link = "/Client/Clients/ClientTrackingAWB_One";

 //                   umiCSearchAWB.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.SearchAWB &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Clients && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiList.Add(umiCSearchAWB);
 //                   }
 //                   #endregion
 //                   #region Shipment Track
 //                   UserMenuItem umiCMulti = new UserMenuItem();
 //                   umiCMulti.Name = "Multi Tracking";
 //                   umiCMulti.Icon = "icon-ShipmentTrack";
 //                   umiCMulti.Link = "/Home/TrackPackage_One";
 //                   umiCMulti.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.SearchAWB &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Clients && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiList.Add(umiCMulti);
 //                   }

 //                   UserMenuItem umiCSingle = new UserMenuItem();
 //                   umiCSingle.Name = "Single Tracking";
 //                   umiCSingle.Link = "/Client/Clients/Track_One";
 //                   umiCSingle.Icon = "icon-ShipmentTrack";
 //                   umiCSingle.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.SearchAWB &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Clients && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiList.Add(umiCSingle);
 //                   }
 //                   #endregion
 //                   #region In Warehouse
 //                   UserMenuItem umiCInWarehouse = new UserMenuItem();
 //                   umiCInWarehouse.Name = "Warehouse";
 //                   umiCInWarehouse.Icon = "icon-InWarehouse";
 //                   umiCInWarehouse.Link = "/Client/Clients/InWarehouse_One";
 //                   umiCInWarehouse.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.SearchAWB &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Clients && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiList.Add(umiCInWarehouse);
 //                   }
 //                   #endregion
 //                   #region Invoices
 //                   UserMenuItem umiCInvoices = new UserMenuItem();
 //                   umiCInvoices.Name = "Invoices";
 //                   umiCInvoices.Icon = "icon-Invoices";
 //                   umiCInvoices.Link = "/Client/Clients/ViewChargeInvoice";
 //                   umiCInvoices.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.ViewInvoice &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Clients && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiList.Add(umiCInvoices);
 //                   }
 //                   #endregion
 //                   #region COD Transfer
                 

 //                   UserMenuItem umiCODPending = new UserMenuItem();
 //                   umiCODPending.Name = "Pending COD";
 //                   umiCODPending.Icon = "icon-COD Transfer";
 //                   umiCODPending.Link = "/Client/Clients/ViewPendingCODInvoice";
 //                   umiCODPending.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.PendingCOD &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Clients && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiList.Add(umiCODPending);
 //                   }

 //                   UserMenuItem umiCODApproved = new UserMenuItem();
 //                   umiCODApproved.Name = "Approved COD";
 //                   umiCODApproved.Icon = "icon-COD Transfer";
 //                   umiCODApproved.Link = "/Client/Clients/ViewCompleteCODInvoice";
 //                   umiCODApproved.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.CompleteCOD &&
 //                p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Clients && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiList.Add(umiCODApproved);
 //                   }
 //                   UserMenuItem umiCCODTransfer = new UserMenuItem();
 //                   umiCCODTransfer.Name = "List Transfer";
 //                   umiCCODTransfer.Icon = "icon-COD Transfer";
 //                   umiCCODTransfer.Link = "/Client/Clients/Transfer";
 //                   umiCCODTransfer.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Transfer &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Clients && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiList.Add(umiCCODTransfer);
 //                   }
 //                   #endregion
 //                   #region Balance
 //                   UserMenuItem umiCBalance = new UserMenuItem();
 //                   umiCBalance.Name = "Balance";
 //                   umiCBalance.Icon = "icon-Balance";
 //                   umiCBalance.Link = "/Client/Clients/Balance";

 //                   umiCBalance.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Balance &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Clients && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiList.Add(umiCBalance);
 //                   }
 //                   #endregion
 //                   #region Pickup Location Client
 //                   UserMenuItem umilocationclient = new UserMenuItem();
 //                   umilocationclient.Name = "Pickup Location";
 //                   umilocationclient.Icon = "icon-star";
 //                   umilocationclient.Link = "/Client/Clients/Add_PickupLoction";
 //                   umilocationclient.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.NewAWB &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Clients && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiList.Add(umilocationclient);
 //                   }

 //                   UserMenuItem umilocationclient2 = new UserMenuItem();
 //                   umilocationclient2.Name = "Manage Location";
 //                   umilocationclient2.Icon = "icon-star";
 //                   umilocationclient2.Link = "/Client/Clients/PickupLocaionMangment";
 //                   umilocationclient2.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.NewAWB &&
 //                   p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Clients && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiList.Add(umilocationclient2);
 //                   }
 //                   #endregion
            
 //                   #region Profile
 //                   UserMenuItem umiCProfile = new UserMenuItem();
 //                   umiCProfile.Name = "Profile";
 //                   umiCProfile.Icon = "icon-Profile";
 //                   umiCProfile.Link = "/Client/Clients/ClientProfile_One";

 //                   umiCProfile.SubItems = new List<UserMenuSubItem>();
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.SearchAWB &&
 //                 p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Clients && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiList.Add(umiCProfile);
 //                   }
 //                   #endregion
                   
 //                   #endregion
 //                   #region منيو الكباتن
 //                   #region Driver Pickup (الالتقاط خاص بالكباتن)
 //                   UserMenuItem umiDriverPickup = new UserMenuItem();
 //                   umiDriverPickup.Name = "Driver Pickup";
 //                   umiDriverPickup.Icon = "icon-star";
 //                   if (repoPermission.GetAll(p => p.PermissionControllerActions.PermissionActionID == (int)ActionEnum.DriverPickup &&
 //                  p.PermissionControllerActions.PermissionControllerID == (int)ControllerEnum.Drivers && userRolesID.Contains(p.RoleId)).FirstOrDefault() != null)
 //                   {
 //                       umiDriverPickup.Link = "/Driver/Drivers/BulkDriverPickup";
 //                   }
 //                   umiDriverPickup.SubItems = new List<UserMenuSubItem>();
 //                   if (umiDriverPickup.Link != null)
 //                   {
 //                       umiList.Add(umiDriverPickup);
 //                   }
 //                   #endregion
 //                   #endregion
 //                   #region (تسجيل الخروج للكل (العملاء - الموظفين - الكباتن
 //                   UserMenuItem umiCSignOut = new UserMenuItem();
 //                   umiCSignOut.Name = "Sign Out";
 //                   umiCSignOut.Icon = "icon-SignOut";
 //                   umiCSignOut.Link = "/Account/LogOff";
 //                   umiCSignOut.SubItems = new List<UserMenuSubItem>();
 //                   umiList.Add(umiCSignOut);
 //                   #endregion
 //               }
 //           }
            return umiList;
        }
    }
}
