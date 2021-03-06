﻿using System.Threading.Tasks;
using Bgc.Controllers;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;

namespace Bgc.Services
{
	/// <summary>
	/// Used to generate new antiforgery token each time used changes (login/logout/start) and validates all post requests. It is safe to not add validation attributes, as it may generate double validation which may affect performance.
	/// </summary>
	[UsedImplicitly]
	public sealed class AntiforgeryMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IAntiforgery _antiforgery;

		private const string AuthTransferRoute = 
			"/api/Account/"+nameof(AccountController.EnsureAuthTransfer);

		public AntiforgeryMiddleware(RequestDelegate next, IAntiforgery antiforgery)
		{
			_next = next;
			_antiforgery = antiforgery;
		}

		/// <summary>
		/// Generates antiforgery token when request is home page, user logs in or out.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		[UsedImplicitly]
		public async Task Invoke(HttpContext context)
		{
			if (context.Request.Path.Value == "/" || IsLogin(context)|| IsLogout(context))
			{
				var tokens = _antiforgery.GetAndStoreTokens(context);
				context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
					new CookieOptions() { HttpOnly = false, Secure = true});
			}
			var valid = await _antiforgery.IsRequestValidAsync(context);
			if (valid)
				await _next.Invoke(context);
		}

		private bool IsLogin(HttpContext context)
		{
			return context.Response.StatusCode == 200
				&& context.User.Identity.IsAuthenticated
				&& context.Request.Path.Value.Equals(AuthTransferRoute);
		}

		private bool IsLogout(HttpContext context)
		{
			return context.Request.Path.Value.Equals(AuthTransferRoute)
				& !context.User.Identity.IsAuthenticated;
		}
	}
}