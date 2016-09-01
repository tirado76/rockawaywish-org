using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using InteractiveMembership.Core.Models;
using InteractiveMembership.Core.Constants;
using InteractiveMembership.Data.Providers;

using RockawayWish.Web.Models;

namespace RockawayWish.Web.Controllers
{
    [Authorize]
    public class MembersController : BaseController
    {
        public MembersController()
        {
            _provider = new UserPaymentsProvider();
            _model = new UserPaymentsModel();
        }
        private UserPaymentsProvider _provider;
        private UserPaymentsModel _model;

        // GET: Members
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Info()
        {
            UserModel model = new UsersProvider().GetById(this.ApplicationId, this.UserId).Result;

            return View(model);

        }
        public ActionResult Payments()
        {
            UserPaymentsViewModel vm = new UserPaymentsViewModel();
            vm.UserId = this.UserId;
            vm.ApplicationId = new Guid(Config.ApplicationId);

            // get user dues list
            List<UserDuesModel> userDuesList = (List<UserDuesModel>)new UserDuesProvider().Get(vm.ApplicationId).Result.Where(x => x.UserId.Equals(vm.UserId)).OrderByDescending(x => x.DuesModel.Title).ToList();

            // get user payments list
            List<UserPaymentsModel> lsUserPayments = (List<UserPaymentsModel>)_provider.Get(vm.ApplicationId).Result.Where(x => x.UserId == vm.UserId).ToList();


            // loop through user dues list and generate dictionary
            foreach (var userDues in userDuesList)
            {
                // get payments for dues
                List<UserPaymentsModel> lsUserPaymentList = (from payment in lsUserPayments
                                                             where payment.UserId == vm.UserId && payment.PaymentId == userDues.DuesId && payment.PaymentTypeId == 1
                                                             select payment).ToList();

                foreach (var userPayment in lsUserPaymentList)
                {
                    userPayment.PaymentDate = userPayment.PaymentDate.Date;
                }
                vm.DuesPaymentList.Add(string.Format("{0}|{1}", userDues.DuesModel.Title, userDues.DuesModel.DuesId), lsUserPaymentList);
            }

            if (userDuesList != null && userDuesList.Count > 0)
                vm.UserModel = userDuesList.FirstOrDefault().UserModel;
            else
                vm.UserModel = new UsersProvider().GetById(vm.ApplicationId, vm.UserId).Result;

            

            return View(vm);
        }

        public ActionResult Payment(Guid paymentId)
        {
            Session["UserPaymentId"] = paymentId;

            UserPaymentsViewModel vm = new UserPaymentsViewModel();
            vm.UserId = this.UserId;
            vm.ApplicationId = this.ApplicationId;

            // get user dues list

            List<UserDuesModel> userDuesList = new List<UserDuesModel>();

            UserDuesModel useModel = new UserDuesProvider().GetById(vm.ApplicationId, vm.UserId, paymentId).Result;
            userDuesList.Add(useModel);
            
            // get user payments list
            List<UserPaymentsModel> lsUserPayments = (List<UserPaymentsModel>)_provider.Get(vm.ApplicationId).Result.Where(x => x.UserId == vm.UserId).ToList();


            // loop through user dues list and generate dictionary
            foreach (var userDues in userDuesList)
            {
                // get payments for dues
                List<UserPaymentsModel> lsUserPaymentList = (from payment in lsUserPayments
                                                             where payment.UserId == vm.UserId && payment.PaymentId == userDues.DuesId && payment.PaymentTypeId == 1
                                                             select payment).ToList();

                foreach (var userPayment in lsUserPaymentList)
                {
                    userPayment.PaymentDate = userPayment.PaymentDate.Date;
                }
                vm.DuesPaymentList.Add(string.Format("{0}|{1}|{2}", userDues.DuesModel.Title, userDues.DuesModel.DuesId, userDues.DuesModel.PaypalButtonId), lsUserPaymentList);

            }


            if (userDuesList != null && userDuesList.Count > 0)
                vm.UserModel = userDuesList.FirstOrDefault().UserModel;
            else
                vm.UserModel = new UsersProvider().GetById(vm.ApplicationId, vm.UserId).Result;


            return View(vm);
        }
        public ActionResult CancelPayment()
        {
            _model = _provider.Create(this.ApplicationId, this.UserId, new Guid(Session["UserPaymentId"].ToString()), 1, "PayPal", DateTime.Now, 50.00m).Result;
            KillOrderSession();
            return View();
        }
        public ActionResult CompletePayment()
        {
            _model = _provider.Create(this.ApplicationId, this.UserId, new Guid(Session["UserPaymentId"].ToString()), 1, "PayPal", DateTime.Now, 50.00m).Result;

            KillOrderSession();
            return View();
        }
        private void KillOrderSession()
        {
            Session["UserPaymentId"] = null;
        }

    }
}