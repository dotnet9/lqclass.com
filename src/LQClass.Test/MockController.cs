﻿using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.TagHelpers.LayUI.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using LQClass.DataAccess;

namespace LQClass.Test
{
    public class MockController
    {
        public static T CreateController<T>(string dataseed, string usercode) where T:BaseController,new()
        {
            var _controller = new T();
            _controller.DC = new DataContext(dataseed, DBTypeEnum.Memory);
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession mockSession = new MockHttpSession();
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            mockHttpContext.Setup(x => x.Request).Returns(new DefaultHttpContext().Request);
            Mock<IServiceProvider> mockService = new Mock<IServiceProvider>();
            mockService.Setup(x => x.GetService(typeof(GlobalData))).Returns(new GlobalData());
            mockService.Setup(x => x.GetService(typeof(Configs))).Returns(new Configs());
            mockService.Setup(x => x.GetService(typeof(IDistributedCache))).Returns(new MemoryDistributedCache(Options.Create<MemoryDistributedCacheOptions>(new MemoryDistributedCacheOptions())));
            GlobalServices.SetServiceProvider(mockService.Object);
            _controller.ControllerContext.HttpContext = mockHttpContext.Object;
            _controller.Cache = new MemoryDistributedCache(Options.Create<MemoryDistributedCacheOptions>(new MemoryDistributedCacheOptions()));
            _controller.LoginUserInfo = new LoginUserInfo { ITCode = usercode ?? "user" };
            _controller.ConfigInfo = new Configs();
            _controller.GlobaInfo = new GlobalData();
            _controller.GlobaInfo.AllAccessUrls = new List<string>();
            _controller.GlobaInfo.AllAssembly = new List<System.Reflection.Assembly>();
            _controller.GlobaInfo.AllModels = new List<Type>();
            _controller.GlobaInfo.SetModuleGetFunc(() => new List<FrameworkModule>());
            _controller.UIService = new LayuiUIService();
            return _controller;
        }

		public static T CreateApi<T>(string dataseed, string usercode) where T : BaseApiController, new()
        {
            var _controller = new T();
            _controller.DC = new DataContext(dataseed, DBTypeEnum.Memory);
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession mockSession = new MockHttpSession();
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            Mock<IServiceProvider> mockService = new Mock<IServiceProvider>();
            mockService.Setup(x => x.GetService(typeof(GlobalData))).Returns(new GlobalData());
            mockService.Setup(x => x.GetService(typeof(Configs))).Returns(new Configs());
            mockService.Setup(x => x.GetService(typeof(IDistributedCache))).Returns(new MemoryDistributedCache(Options.Create<MemoryDistributedCacheOptions>(new MemoryDistributedCacheOptions())));
            GlobalServices.SetServiceProvider(mockService.Object);
            _controller.ControllerContext.HttpContext = mockHttpContext.Object;
            _controller.Cache = new MemoryDistributedCache(Options.Create<MemoryDistributedCacheOptions>(new MemoryDistributedCacheOptions()));
            _controller.LoginUserInfo = new LoginUserInfo { ITCode = usercode ?? "user" };
            _controller.ConfigInfo = new Configs();
            _controller.GlobaInfo = new GlobalData();
            _controller.GlobaInfo.AllAccessUrls = new List<string>();
            _controller.GlobaInfo.AllAssembly = new List<System.Reflection.Assembly>();
            _controller.GlobaInfo.AllModels = new List<Type>();
            _controller.GlobaInfo.SetModuleGetFunc(() => new List<FrameworkModule>());
            return _controller;
        }

    }
}
