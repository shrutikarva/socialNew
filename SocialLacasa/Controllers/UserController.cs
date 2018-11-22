﻿using SocialLacasa.DataLayer;
using SocialLacasa.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialLacasa.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Account()
        {
            return View();
        }
        public ActionResult FAQ()
        {
            return View();
        }
        public ActionResult Orders(string status="")
        {
            DataTable dtorders = new DataTable();
            
            var objUser = new User();
            dtorders = objUser.Getorders(Session["UserId"].ToString(), status);
            DataSet ds = new DataSet();
            ds.Tables.Add(dtorders);
            return View(ds);
        }
        public ActionResult MassOrders()
        {
            return View();
        }
        public ActionResult AddFunds()
        {
            return View();
        }
        public ActionResult Subscriptions()
        {
            return View();
        }
        public ActionResult NewOrder()
        {
            var objUser = new User();
            DataTable dtCategory = objUser.GetAllCategory();
            ViewBag.CatagoryName = new SelectList(dtCategory.AsDataView(), "CatagoryId", "CatagoryName");//dtMessages.AsEnumerable().ToList();




            return View();
        }

        public ActionResult Tickets() {
            var objUser = new User();
            DataTable dtTickets = objUser.GetAllTicketsForUser(Session["UserId"].ToString());
            var myEnumerable = dtTickets.AsEnumerable();

            List<Tickets> lstTickets =
                (from item in myEnumerable
                 select new Tickets
                 {
                     TicketId = item.Field<Int32>("TicketId"),
                     UserId = item.Field<Int32>("UserId"),
                     Subject = item.Field<string>("Subject"),
                     TicketMessage = item.Field<string>("TicketMessage"),
                     Status = item.Field<string>("Status"),
                     Date = item.Field<DateTime>("Date"),

                 }).ToList();
            return View(lstTickets);
        }


        public ActionResult TicketDetails(string TicketId) {
            var objuser = new User();
            DataTable dtMessages = objuser.GetTicketConversation(Session["UserId"].ToString(), TicketId);
            var myEnumerable = dtMessages.AsEnumerable();

            List<Messages> lstMessages =
                (from item in myEnumerable
                 select new Messages
                 {
                     Subject = item.Field<string>("Subject"),
                     TicketMessage = item.Field<string>("TicketMessage"),
                     Date = item.Field<DateTime>("Date"),
                     Message = item.Field<string>("Message"),
                     MessageDate = item.Field<DateTime>("MessageDate"),
                     SentByCustomer = item.Field<bool>("SentByCustomer"),
                     TicketId = item.Field<int>("TicketId"),
                 }).ToList();
            return View(lstMessages);

        }

        public ActionResult Terms()
        {
            return View();
        }
        public ActionResult Services()
        {
            var objUser = new User();
            DataTable dtServices = objUser.GetAllServiceCategory();
            var myEnumerable = dtServices.AsEnumerable();

            List<Services> lstServices =
                (from item in myEnumerable
                 select new Services
                 {
                     Category = item.Field<string>("Category"),
                     SWserviceId = item.Field<Int32>("SWserviceId"),
                     ServiceType = item.Field<string>("ServiceType"),
                     Rate=item.Field<decimal>("Rate"),
                     MinOrder = item.Field<Int32>("MinOrder"),
                     MaxOrder = item.Field<Int32>("MaxOrder"),
                     Description = item.Field<string>("Description"),
                 }).ToList();
            return View(lstServices);
        }
    }
}