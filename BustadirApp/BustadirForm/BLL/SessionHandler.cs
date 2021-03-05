//using BustadirForm.DAL;
using BustadirForm.Models;
using Newtonsoft.Json;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BustadirForm.BLL
{
    public class SessionHandler
    {
        private static SessionHandler _instance = null;
        public static SessionHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SessionHandler();
                }
                return _instance;
            }
        }

        private SessionHandler() { }

        private SessionOrder Create(HttpSessionStateBase Session, HttpRequestBase request)
        {
            var sessionId = Session.SessionID;
            var date = DateTime.Now;
            try
            {
                var fileNameAllOrders = request.MapPath("~/App_Data/Orders/all.json");
                var fileAllOrders = System.IO.File.ReadAllText(fileNameAllOrders, System.Text.Encoding.UTF8);
                var allOrders = JsonConvert.DeserializeObject<List<SessionOrder>>(fileAllOrders);
                var lastOrder = allOrders.Where(x => x.Date == allOrders.Max(y => y.Date)).FirstOrDefault();

                var fileName = request.MapPath("~/App_Data/Orders/" + sessionId + ".json");
                var sessionOrder = new SessionOrder()
                {
                    Id = sessionId,
                    Date = DateTime.Now,
                    OrderId = lastOrder.OrderId + 1
                };
                System.IO.File.WriteAllText(fileName, JsonConvert.SerializeObject(sessionOrder), System.Text.Encoding.UTF8);

                allOrders.Add(sessionOrder);
                System.IO.File.WriteAllText(fileNameAllOrders, JsonConvert.SerializeObject(allOrders), System.Text.Encoding.UTF8);
                return sessionOrder;
            }
            catch (Exception)
            {
                // todo: should be handled :)
                return null;
            }
        }

        public SessionOrder Read(HttpSessionStateBase Session, HttpRequestBase request)
        {
            var sessionId = Session.SessionID;
            try
            {
                var fileName = request.MapPath("~/App_Data/Orders/" + sessionId + ".json");
                var file = System.IO.File.ReadAllText(fileName, System.Text.Encoding.UTF8);
                return JsonConvert.DeserializeObject<SessionOrder>(file);
            }
            catch (Exception)
            {
                return Create(Session, request);
            }
        }

        public void Destroy(HttpSessionStateBase session, HttpRequestBase request)
        {
            Destroy(session.SessionID, session, request);
        }

        public void Destroy(string id, HttpSessionStateBase session, HttpRequestBase request)
        {
            session.Abandon();
            try
            {
                var fileName = request.MapPath("~/App_Data/Orders/" + id + ".json");
                System.IO.File.Delete(fileName);
            }
            catch (Exception)
            {
                // todo: to be handled!
            }
        }
    }
}