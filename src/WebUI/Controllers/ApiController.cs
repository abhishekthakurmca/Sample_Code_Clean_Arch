using Anubis.Application.Common.Interfaces;
using AutoMapper.Configuration;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Anubis.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator;
        private IFileService _fileService;
        private IEmailService _emailService;
        private ICurrentUserService _currentUserService;
        private IConfiguration _configuration;
        private IGoogleService _googleService;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected IFileService FileService => _fileService ??= HttpContext.RequestServices.GetService<IFileService>();
        protected IEmailService EmailService => _emailService ??= HttpContext.RequestServices.GetService<IEmailService>();
        protected ICurrentUserService CurrentUserService => _currentUserService ??= HttpContext.RequestServices.GetService<ICurrentUserService>();
        protected IConfiguration Configuration => _configuration ??= HttpContext.RequestServices.GetService<IConfiguration>();
        protected IGoogleService GoogleService => _googleService ??= HttpContext.RequestServices.GetService<IGoogleService>();

    }
}
