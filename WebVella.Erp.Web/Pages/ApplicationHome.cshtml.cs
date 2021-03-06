﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WebVella.Erp.Diagnostics;
using WebVella.Erp.Hooks;
using WebVella.Erp.Web.Hooks;
using WebVella.Erp.Web.Models;

namespace WebVella.Erp.Web.Pages.Application
{
	[Authorize]
	public class ApplicationHomePageModel : BaseErpPageModel
	{
		public ApplicationHomePageModel([FromServices]ErpRequestContext reqCtx) { ErpRequestContext = reqCtx; }

		public IActionResult OnGet()
		{
			try
			{
				Init();
				if (ErpRequestContext.Page == null) return NotFound();

				var globalHookInstances = HookManager.GetHookedInstances<IPageHook>(HookKey);
				foreach (IPageHook inst in globalHookInstances)
				{
					var result = inst.OnGet(this);
					if (result != null) return result;
				}

				foreach (IApplicationHomePageHook inst in HookManager.GetHookedInstances<IApplicationHomePageHook>(HookKey))
				{
					var result = inst.OnGet(this);
					if (result != null)
						return result;
				}
				BeforeRender();
				return Page();
			}
			catch (Exception ex)
			{
				new Log().Create(LogType.Error, "ApplicationHomePageModel Error on GET", ex);
				BeforeRender();
				return Page();
			}
		}

		public IActionResult OnPost()
		{
			try
			{
				if (!ModelState.IsValid) throw new Exception("Antiforgery check failed.");
				Init();

				if (ErpRequestContext.Page == null) return NotFound();

				var globalHookInstances = HookManager.GetHookedInstances<IPageHook>(HookKey);
				foreach (IPageHook inst in globalHookInstances)
				{
					var result = inst.OnPost(this);
					if (result != null) return result;
				}

				foreach (IApplicationHomePageHook inst in HookManager.GetHookedInstances<IApplicationHomePageHook>(HookKey))
				{
					var result = inst.OnPost(this);
					if (result != null)
						return result;
				}
				BeforeRender();
				return Page();
			}
			catch (Exception ex)
			{
				new Log().Create(LogType.Error, "ApplicationHomePageModel Error on POST", ex);
				BeforeRender();
				return Page();
			}
		}

	}
}

/*
 * system actions: none
 * custom actions: on post based on handler name
 */
