﻿using System.Text;
using System.Web.Mvc;
using Application.Core;
using Application.Core.SLExcel;
using Application.Infrastructure.GroupManagement;
using Application.Infrastructure.LecturerManagement;
using Application.Infrastructure.SubjectManagement;
using LMPlatform.UI.Services.Labs;

namespace LMPlatform.UI.Controllers
{
	public class StatisticController : Controller
	{
		private readonly LazyDependency<IGroupManagementService> groupManagementService = new LazyDependency<IGroupManagementService>();

		public IGroupManagementService GroupManagementService
		{
			get
			{
				return groupManagementService.Value;
			}
		}

		private readonly LazyDependency<ILecturerManagementService> lecturerManagementService = new LazyDependency<ILecturerManagementService>();

		public ILecturerManagementService LecturerManagementService
		{
			get
			{
				return lecturerManagementService.Value;
			}
		}

		public void GetVisitLecture(int subjectId, int groupId)
		{
			var data = new SLExcelData();

			var headerData = LecturerManagementService.GetLecturesScheduleVisitings(subjectId);
			var rowsData = LecturerManagementService.GetLecturesScheduleMarks(subjectId, groupId);

			data.Headers.Add("Студент");
			data.Headers.AddRange(headerData);
			data.DataRows.AddRange(rowsData);
			
			var file = (new SLExcelWriter()).GenerateExcel(data);

			Response.Clear();
			Response.Charset = "ru-ru";
			Response.HeaderEncoding = Encoding.UTF8;
			Response.ContentEncoding = Encoding.UTF8;
			Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
			Response.AddHeader("Content-Disposition", "attachment; filename=LectureVisiting.xlsx");
			Response.BinaryWrite(file);
			Response.Flush();
			Response.End();
		}

		public void GetVisitLabs(int subjectId, int groupId, int subGroupId)
		{
			var data = new SLExcelData();

			var headerData = GroupManagementService.GetLabsScheduleVisitings(subjectId, groupId, subGroupId);
			var rowsData = GroupManagementService.GetLabsScheduleMarks(subjectId, groupId, subGroupId);

			data.Headers.Add("Студент");
			data.Headers.AddRange(headerData);
			data.DataRows.AddRange(rowsData);

			var file = (new SLExcelWriter()).GenerateExcel(data);

			Response.Clear();
			Response.Charset = "ru-ru";
			Response.HeaderEncoding = Encoding.UTF8;
			Response.ContentEncoding = Encoding.UTF8;
			Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
			Response.AddHeader("Content-Disposition", "attachment; filename=LabVisiting.xlsx");
			Response.BinaryWrite(file);
			Response.Flush();
			Response.End();
		}
	}
}