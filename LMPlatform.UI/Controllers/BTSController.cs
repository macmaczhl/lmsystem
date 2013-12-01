﻿using System;
using System.Linq;
using System.Web.Mvc;
using Application.Core.UI.Controllers;
using Application.Core.UI.HtmlHelpers;
using Application.Infrastructure.ProjectManagement;
using LMPlatform.UI.ViewModels.BTSViewModels;
using Mvc.JQuery.Datatables;

namespace LMPlatform.UI.Controllers
{
    [Authorize(Roles = "student, lector")]
    public class BTSController : BasicController
    {
        [HttpGet]
        public ActionResult Projects()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Projects(ProjectsViewModel model)
        {
            model.SaveProject();
            return View();
        }

        [HttpGet]
        public ActionResult ProjectManagement()
        {
            var model = new ProjectsViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult ProjectManagement(int projectId)
        {
            var model = new ProjectsViewModel(projectId);
            return View(model);
        }

        public ActionResult ErrorManagement()
        {
            return View();
        }

        [HttpPost]
        public DataTablesResult<ProjectListViewModel> GetAllProjects(DataTablesParam dataTableParam)
        {
            var searchString = dataTableParam.GetSearchString();
            var projects = ProjectManagementService.GetAllProjects(pageInfo: dataTableParam.ToPageInfo(),
                searchString: searchString);

            return DataTableExtensions.GetResults(projects.Items.Select(ProjectListViewModel.FromProject), dataTableParam, projects.TotalCount);
        }

        [HttpPost]
        public DataTablesResult<ProjectListViewModel> GetChosenProjects(DataTablesParam dataTableParam)
        {
            var searchString = dataTableParam.GetSearchString();
            var projects = ProjectManagementService.GetChosenProjects(pageInfo: dataTableParam.ToPageInfo(),
                searchString: searchString);

            return DataTableExtensions.GetResults(projects.Items.Select(ProjectListViewModel.FromProject), dataTableParam, projects.TotalCount);
        }

        public IProjectManagementService ProjectManagementService
        {
            get
            {
                return ApplicationService<IProjectManagementService>();
            }
        }
    }
}
