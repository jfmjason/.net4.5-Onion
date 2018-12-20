using AutoMapper;
using LightInject;
using Sghis.Core.Entity.Base;
using Sghis.Core.Entity.QmsClient.Interface;
using Sghis.QmsClient.Infra.Business;
using Sghis.QmsClient.Infra.Data;
using Sghis.QmsClient.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Sghis.QmsClient.UI
{
    public class LightningMcQueen
    {
        private static ServiceContainer container;

        public static void Flash()
        {
            container = new ServiceContainer();
            container.RegisterApiControllers();
            container.EnablePerWebRequestScope();
            container.EnableWebApi(GlobalConfiguration.Configuration);

            container.Register<QmsClientDbContext>();
            container.Register(typeof(IRepository<>),typeof(QmsClientRepository<>));
            container.Register(typeof(IDbHelper<>), typeof(QmsClientDbHelper<>));
            container.Register<IQmsClientDataManager, QmsClientDataManager>();

            //Business or Services
            container.Register<IQmsClientMasterFileBusiness, QmsClientMasterFileBusiness>();
            container.Register<IQmsClientUserBusiness, QmsClientUserBusiness>();

            //Config automapper profiles
            var config = new MapperConfiguration(cfg => { 
                cfg.AddProfiles(typeof(MasterFileMappingProfile));
                cfg.AddProfiles(typeof(UserMappingProfile));
             });

            //register automapper 
            container.Register(c => config.CreateMapper());

            container.RegisterControllers(typeof(MvcApplication).Assembly);

            container.EnableMvc();


        }

        public static ServiceContainer Container { get { return container; } }

    }
}