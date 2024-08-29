using Buyonida.Business.DTOs.UserDTOs;
using Buyonida.Business.Exceptions;
using Buyonida.Business.Services.Abstracts;
using Buyonida.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Buyonida_T2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserAccountController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserAccountController> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMailService _mailService;
    private readonly ITokenService _tokenService;
    private readonly IPersonalService _personalService;
    private readonly IInvidualService _invidualService;
    private readonly IJuridicalService _juridicalService;

    public UserAccountController(IUserService userService, ILogger<UserAccountController> logger, UserManager<AppUser> userManager, IMailService mailService, ITokenService tokenService, IPersonalService personalService, IInvidualService invidualService, IJuridicalService juridicalService)
    {
        _userService = userService;
        _logger = logger;
        _userManager = userManager;
        _mailService = mailService;
        _tokenService = tokenService;
        _personalService = personalService;
        _invidualService = invidualService;
        _juridicalService = juridicalService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] InitialRegisterDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Invalid input data"
            });
        }
        
        try
        {
            await _userService.Register(registerDto);

            // Kullanıcıyı bul
            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            if (user == null)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "User registration failed."
                });
            }
            //var roleResult = await _userManager.AddToRoleAsync(user, "User");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);


            string link = Url.Action("ConfirmEmail", "UserAccount", new { userId = user.Id, token = token }, HttpContext.Request.Scheme);


            await _mailService.SendEmailAsync(new MailRequest
            {
                Subject = "Confirm Email",
                ToEmail = registerDto.Email,
                //Body = $"\r\n    <!DOCTYPE HTML PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional //EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\">\r\n\r\n<head>\r\n  <!--[if gte mso 9]>\r\n<xml>\r\n  <o:OfficeDocumentSettings>\r\n    <o:AllowPNG/>\r\n    <o:PixelsPerInch>96</o:PixelsPerInch>\r\n  </o:OfficeDocumentSettings>\r\n</xml>\r\n<![endif]-->\r\n  <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\r\n  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n  <meta name=\"x-apple-disable-message-reformatting\">\r\n  <!--[if !mso]><!-->\r\n  <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n  <!--<![endif]-->\r\n  <title></title>\r\n\r\n  <style type=\"text/css\">\r\n    @media only screen and (min-width: 620px) {{\r\n      .u-row {{\r\n        width: 600px !important;\r\n      }}\r\n      .u-row .u-col {{\r\n        vertical-align: top;\r\n      }}\r\n      .u-row .u-col-100 {{\r\n        width: 600px !important;\r\n      }}\r\n    }}\r\n    \r\n    @media (max-width: 620px) {{\r\n      .u-row-container {{\r\n        max-width: 100% !important;\r\n        padding-left: 0px !important;\r\n        padding-right: 0px !important;\r\n      }}\r\n      .u-row .u-col {{\r\n        min-width: 320px !important;\r\n        max-width: 100% !important;\r\n        display: block !important;\r\n      }}\r\n      .u-row {{\r\n        width: 100% !important;\r\n      }}\r\n      .u-col {{\r\n        width: 100% !important;\r\n      }}\r\n      .u-col>div {{\r\n        margin: 0 auto;\r\n      }}\r\n    }}\r\n    \r\n    body {{\r\n      margin: 0;\r\n      padding: 0;\r\n    }}\r\n    \r\n    table,\r\n    tr,\r\n    td {{\r\n      vertical-align: top;\r\n      border-collapse: collapse;\r\n    }}\r\n    \r\n    p {{\r\n      margin: 0;\r\n    }}\r\n    \r\n    .ie-container table,\r\n    .mso-container table {{\r\n      table-layout: fixed;\r\n    }}\r\n    \r\n    * {{\r\n      line-height: inherit;\r\n    }}\r\n    \r\n    a[x-apple-data-detectors='true'] {{\r\n      color: inherit !important;\r\n      text-decoration: none !important;\r\n    }}\r\n    \r\n    table,\r\n    td {{\r\n      color: #000000;\r\n    }}\r\n    \r\n    #u_body a {{\r\n      color: #0000ee;\r\n      text-decoration: underline;\r\n    }}\r\n  </style>\r\n\r\n\r\n\r\n  <!--[if !mso]><!-->\r\n  <link href=\"https://fonts.googleapis.com/css?family=Cabin:400,700\" rel=\"stylesheet\" type=\"text/css\">\r\n  <!--<![endif]-->\r\n\r\n</head>\r\n\r\n<body className=\"clean-body u_body\" style=\"margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #f9f9f9;color: #000000\">\r\n  <!--[if IE]><div className=\"ie-container\"><![endif]-->\r\n  <!--[if mso]><div className=\"mso-container\"><![endif]-->\r\n  <table id=\"u_body\" style=\"border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #f9f9f9;width:100%\" cellpadding=\"0\" cellspacing=\"0\">\r\n    <tbody>\r\n      <tr style=\"vertical-align: top\">\r\n        <td style=\"word-break: break-word;border-collapse: collapse !important;vertical-align: top\">\r\n          <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td align=\"center\" style=\"background-color: #f9f9f9;\"><![endif]-->\r\n\r\n\r\n\r\n          <div className=\"u-row-container\" style=\"padding: 0px;background-color: transparent\">\r\n            <div className=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;\">\r\n              <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n                <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding: 0px;background-color: transparent;\" align=\"center\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:600px;\"><tr style=\"background-color: transparent;\"><![endif]-->\r\n\r\n                <!--[if (mso)|(IE)]><td align=\"center\" width=\"600\" style=\"width: 600px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\" valign=\"top\"><![endif]-->\r\n                <div className=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n                  <div style=\"height: 100%;width: 100% !important;\">\r\n                    <!--[if (!mso)&(!IE)]><!-->\r\n                    <div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\">\r\n                      <!--<![endif]-->\r\n\r\n                      <table style=\"font-family:'Cabin',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td style=\"overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:'Cabin',sans-serif;\" align=\"left\">\r\n\r\n                              <div style=\"font-size: 14px; color: #afb0c7; line-height: 170%; text-align: center; word-wrap: break-word;\">\r\n                                <p style=\"font-size: 14px; line-height: 170%;\"><span style=\"font-size: 14px; line-height: 23.8px;\">View Email in Browser</span></p>\r\n                              </div>\r\n\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table>\r\n\r\n                      <!--[if (!mso)&(!IE)]><!-->\r\n                    </div>\r\n                    <!--<![endif]-->\r\n                  </div>\r\n                </div>\r\n                <!--[if (mso)|(IE)]></td><![endif]-->\r\n                <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n              </div>\r\n            </div>\r\n          </div>\r\n\r\n\r\n\r\n\r\n\r\n          <div className=\"u-row-container\" style=\"padding: 0px;background-color: transparent\">\r\n            <div className=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;\">\r\n              <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n                <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding: 0px;background-color: transparent;\" align=\"center\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:600px;\"><tr style=\"background-color: #ffffff;\"><![endif]-->\r\n\r\n                <!--[if (mso)|(IE)]><td align=\"center\" width=\"600\" style=\"width: 600px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\" valign=\"top\"><![endif]-->\r\n                <div className=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n                  <div style=\"height: 100%;width: 100% !important;\">\r\n                    <!--[if (!mso)&(!IE)]><!-->\r\n                    <div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\">\r\n                      <!--<![endif]-->\r\n\r\n                      <!--[if (!mso)&(!IE)]><!-->\r\n                    </div>\r\n                    <!--<![endif]-->\r\n                  </div>\r\n                </div>\r\n                <!--[if (mso)|(IE)]></td><![endif]-->\r\n                <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n              </div>\r\n            </div>\r\n          </div>\r\n\r\n\r\n\r\n\r\n\r\n          <div className=\"u-row-container\" style=\"padding: 0px;background-color: transparent\">\r\n            <div className=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #003399;\">\r\n              <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n                <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding: 0px;background-color: transparent;\" align=\"center\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:600px;\"><tr style=\"background-color: #003399;\"><![endif]-->\r\n\r\n                <!--[if (mso)|(IE)]><td align=\"center\" width=\"600\" style=\"width: 600px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\" valign=\"top\"><![endif]-->\r\n                <div className=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n                  <div style=\"height: 100%;width: 100% !important;\">\r\n                    <!--[if (!mso)&(!IE)]><!-->\r\n                    <div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\">\r\n                      <!--<![endif]-->\r\n\r\n                      <table style=\"font-family:'Cabin',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td style=\"overflow-wrap:break-word;word-break:break-word;padding:40px 10px 10px;font-family:'Cabin',sans-serif;\" align=\"left\">\r\n\r\n                              <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n                                <tr>\r\n                                  <td style=\"padding-right: 0px;padding-left: 0px;\" align=\"center\">\r\n\r\n                                    <img align=\"center\" border=\"0\" src=\"https://cdn.templates.unlayer.com/assets/1597218650916-xxxxc.png\" alt=\"Image\" title=\"Image\" style=\"outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 26%;max-width: 150.8px;\"\r\n                                      width=\"150.8\" />\r\n\r\n                                  </td>\r\n                                </tr>\r\n                              </table>\r\n\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table>\r\n\r\n                      <table style=\"font-family:'Cabin',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td style=\"overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:'Cabin',sans-serif;\" align=\"left\">\r\n\r\n                              <div style=\"font-size: 14px; color: #e5eaf5; line-height: 140%; text-align: center; word-wrap: break-word;\">\r\n                              </div>\r\n\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table>\r\n\r\n                      <table style=\"font-family:'Cabin',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td style=\"overflow-wrap:break-word;word-break:break-word;padding:0px 10px 31px;font-family:'Cabin',sans-serif;\" align=\"left\">\r\n\r\n                              <div style=\"font-size: 14px; color: #e5eaf5; line-height: 140%; text-align: center; word-wrap: break-word;\">\r\n                                <p style=\"font-size: 14px; line-height: 140%;\"><span style=\"font-size: 28px; line-height: 39.2px;\"><strong><span style=\"line-height: 39.2px; font-size: 28px;\">\r\n                                </span></strong>\r\n                                  </span>\r\n                                </p>\r\n                              </div>\r\n\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table>\r\n\r\n                      <!--[if (!mso)&(!IE)]><!-->\r\n                    </div>\r\n                    <!--<![endif]-->\r\n                  </div>\r\n                </div>\r\n                <!--[if (mso)|(IE)]></td><![endif]-->\r\n                <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n              </div>\r\n            </div>\r\n          </div>\r\n\r\n\r\n\r\n\r\n\r\n          <div className=\"u-row-container\" style=\"padding: 0px;background-color: transparent\">\r\n            <div className=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;\">\r\n              <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n                <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding: 0px;background-color: transparent;\" align=\"center\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:600px;\"><tr style=\"background-color: #ffffff;\"><![endif]-->\r\n\r\n                <!--[if (mso)|(IE)]><td align=\"center\" width=\"600\" style=\"width: 600px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\" valign=\"top\"><![endif]-->\r\n                <div className=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n                  <div style=\"height: 100%;width: 100% !important;\">\r\n                    <!--[if (!mso)&(!IE)]><!-->\r\n                    <div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\">\r\n                      <!--<![endif]-->\r\n\r\n                      <table style=\"font-family:'Cabin',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td style=\"overflow-wrap:break-word;word-break:break-word;padding:33px 55px;font-family:'Cabin',sans-serif;\" align=\"left\">\r\n\r\n                              <div style=\"font-size: 14px; line-height: 160%; text-align: center; word-wrap: break-word;\">\r\n                                <p style=\"font-size: 14px; line-height: 160%;\"><span style=\"font-size: 18px; line-height: 28.8px;\"><a href='{link}'>Reset Password</a> </span></p>\r\n                              </div>\r\n\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table>\r\n\r\n                      <table style=\"font-family:'Cabin',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td style=\"overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:'Cabin',sans-serif;\" align=\"left\">\r\n\r\n                              <!--[if mso]><style>.v-button {{background: transparent !important;}}</style><![endif]-->\r\n                              <div align=\"center\">\r\n                                <!--[if mso]><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"\" style=\"height:46px; v-text-anchor:middle; width:234px;\" arcsize=\"8.5%\"  stroke=\"f\" fillcolor=\"#ff6600\"><w:anchorlock/><center style=\"color:#FFFFFF;\"><![endif]-->\r\n                                <!--[if mso]></center></v:roundrect><![endif]-->\r\n                              </div>\r\n\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table>\r\n\r\n                      <table style=\"font-family:'Cabin',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td style=\"overflow-wrap:break-word;word-break:break-word;padding:33px 55px 60px;font-family:'Cabin',sans-serif;\" align=\"left\">\r\n\r\n                              <div style=\"font-size: 14px; line-height: 160%; text-align: center; word-wrap: break-word;\">\r\n                                <p style=\"line-height: 160%; font-size: 14px;\"><span style=\"font-size: 18px; line-height: 28.8px;\">Thanks,</span></p>\r\n                                <p style=\"line-height: 160%; font-size: 14px;\"><span style=\"font-size: 18px; line-height: 28.8px;\">{user.Name + " " + user.Surname}</span></p>\r\n                              </div>\r\n\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table>\r\n\r\n                      <!--[if (!mso)&(!IE)]><!-->\r\n                    </div>\r\n                    <!--<![endif]-->\r\n                  </div>\r\n                </div>\r\n                <!--[if (mso)|(IE)]></td><![endif]-->\r\n                <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n              </div>\r\n            </div>\r\n          </div>\r\n\r\n\r\n\r\n\r\n\r\n          <div className=\"u-row-container\" style=\"padding: 0px;background-color: transparent\">\r\n            <div className=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #e5eaf5;\">\r\n              <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n                <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding: 0px;background-color: transparent;\" align=\"center\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:600px;\"><tr style=\"background-color: #e5eaf5;\"><![endif]-->\r\n\r\n                <!--[if (mso)|(IE)]><td align=\"center\" width=\"600\" style=\"width: 600px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\" valign=\"top\"><![endif]-->\r\n                <div className=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n                  <div style=\"height: 100%;width: 100% !important;\">\r\n                    <!--[if (!mso)&(!IE)]><!-->\r\n                    <div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\">\r\n                      <!--<![endif]-->\r\n\r\n                      <table style=\"font-family:'Cabin',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td style=\"overflow-wrap:break-word;word-break:break-word;padding:41px 55px 18px;font-family:'Cabin',sans-serif;\" align=\"left\">\r\n\r\n                              <div style=\"font-size: 14px; color: #003399; line-height: 160%; text-align: center; word-wrap: break-word;\">\r\n                                <p style=\"font-size: 14px; line-height: 160%;\"><span style=\"font-size: 16px; line-height: 25.6px; color: #000000;\">{user.Email}</span></p>\r\n                              </div>\r\n                              <div style=\"font-size: 14px; color: #003399; line-height: 160%; text-align: center; word-wrap: break-word;\">\r\n         \r\n                            </div>\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table>\r\n\r\n                      <table style=\"font-family:'Cabin',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 10px 33px;font-family:'Cabin',sans-serif;\" align=\"left\">\r\n\r\n                              <div align=\"center\">\r\n                                <div style=\"display: table; max-width:244px;\">\r\n                                  <!--[if (mso)|(IE)]><table width=\"244\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"border-collapse:collapse;\" align=\"center\"><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-collapse:collapse; mso-table-lspace: 0pt;mso-table-rspace: 0pt; width:244px;\"><tr><![endif]-->\r\n\r\n\r\n                                  <!--[if (mso)|(IE)]><td width=\"32\" style=\"width:32px; padding-right: 17px;\" valign=\"top\"><![endif]-->\r\n                           \r\n                                  <!--[if (mso)|(IE)]></td><![endif]-->\r\n\r\n                                  <!--[if (mso)|(IE)]><td width=\"32\" style=\"width:32px; padding-right: 17px;\" valign=\"top\"><![endif]-->\r\n                              \r\n                                  <!--[if (mso)|(IE)]></td><![endif]-->\r\n\r\n                                  <!--[if (mso)|(IE)]><td width=\"32\" style=\"width:32px; padding-right: 17px;\" valign=\"top\"><![endif]-->\r\n                            \r\n                                  <!--[if (mso)|(IE)]></td><![endif]-->\r\n\r\n                                  <!--[if (mso)|(IE)]><td width=\"32\" style=\"width:32px; padding-right: 17px;\" valign=\"top\"><![endif]-->\r\n                        \r\n                                  <!--[if (mso)|(IE)]></td><![endif]-->\r\n\r\n                                  <!--[if (mso)|(IE)]><td width=\"32\" style=\"width:32px; padding-right: 0px;\" valign=\"top\"><![endif]-->\r\n                            \r\n                                  <!--[if (mso)|(IE)]></td><![endif]-->\r\n\r\n\r\n                                  <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n                                </div>\r\n                              </div>\r\n\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table>\r\n\r\n                      <!--[if (!mso)&(!IE)]><!-->\r\n                    </div>\r\n                    <!--<![endif]-->\r\n                  </div>\r\n                </div>\r\n                <!--[if (mso)|(IE)]></td><![endif]-->\r\n                <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n              </div>\r\n            </div>\r\n          </div>\r\n\r\n\r\n\r\n\r\n\r\n          <div className=\"u-row-container\" style=\"padding: 0px;background-color: transparent\">\r\n            <div className=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;\">\r\n              <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n                <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding: 0px;background-color: transparent;\" align=\"center\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:600px;\"><tr style=\"background-color: #003399;\"><![endif]-->\r\n\r\n                <!--[if (mso)|(IE)]><td align=\"center\" width=\"600\" style=\"width: 600px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\" valign=\"top\"><![endif]-->\r\n                <div className=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n                  <div style=\"height: 100%;width: 100% !important;\">\r\n                    <!--[if (!mso)&(!IE)]><!-->\r\n                    <div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\">\r\n                      <!--<![endif]-->\r\n\r\n                      <table style=\"font-family:'Cabin',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td style=\"overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:'Cabin',sans-serif;\" align=\"left\">\r\n\r\n                              <div style=\"font-size: 14px; color: #fafafa; line-height: 180%; text-align: center; word-wrap: break-word;\">\r\n                              </div>\r\n\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table>\r\n\r\n                      <!--[if (!mso)&(!IE)]><!-->\r\n                    </div>\r\n                    <!--<![endif]-->\r\n                  </div>\r\n                </div>\r\n                <!--[if (mso)|(IE)]></td><![endif]-->\r\n                <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n              </div>\r\n            </div>\r\n          </div>\r\n\r\n\r\n\r\n          <!--[if (mso)|(IE)]></td></tr></table><![endif]-->\r\n        </td>\r\n      </tr>\r\n    </tbody>\r\n  </table>\r\n  <!--[if mso]></div><![endif]-->\r\n  <!--[if IE]></div><![endif]-->\r\n</body>\r\n\r\n</html>\r\n"
                Body = $"<a href='{link}'>Confirm Email</a>"

            });

            return StatusCode(StatusCodes.Status201Created, new
            {
                StatusCode = StatusCodes.Status201Created,
                Message = "User registered successfully!"
            });
        }
        catch (GlobalAppException ex)
        {
            _logger.LogError(ex, "An error occurred while registering the user");
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred");
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = "An unexpected error occurred. Please try again later."
            });
        }
    }


    [HttpGet("confirmemail")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token, [FromQuery] string userId)
    {
        _logger.LogInformation($"ConfirmEmail called with token: {token} and userId: {userId}");

        if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId))
        {
            _logger.LogWarning("Invalid token or user ID.");
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Error = "Invalid token or user ID."
            });
        }

        

        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning($"User not found with userId: {userId}");
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = "Invalid token or user ID."
                });
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                _logger.LogInformation($"Email confirmed successfully for userId: {userId}");
                return Redirect("https://frontend-final-navy.vercel.app/email-verify");
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogWarning($"Email confirmation failed for userId: {userId}, errors: {errors}");
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = $"Email confirmation failed: {errors}"
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while confirming the email");
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Error = "An unexpected error occurred. Please try again later."
            });
        }
    }

    [HttpPost("login")]
    public async Task<TokenResponseDto> LoginAsync(LoginDto dto)
    {
        try
        {
            var user = await _userManager.Users
                .Where(u => (u.UserName == dto.UserNameOrEmail || u.Email == dto.UserNameOrEmail) && !u.IsDeleted)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new GlobalAppException("User not found with the provided username or email.");
            }

            if (!user.EmailConfirmed)
            {
                throw new GlobalAppException("Email not verified. Please confirm your email.");
            }
            

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!isPasswordValid)
            {
                throw new GlobalAppException("Invalid password.");
            }

            var tokenResponse = _tokenService.CreateToken(user);

            return tokenResponse;
        }
        catch (GlobalAppException ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while logging in the user");
            throw new GlobalAppException("An unexpected error occurred while logging in the user", ex);
        }
    }


    [HttpPost("personal-info")]
    [Authorize]
    public async Task<IActionResult> PersonalInfo(RegisterPersonalInfoDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.UserId);
        if(user is null)
        {
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "User Not Found!"
            });
        }
        if (!user.EmailConfirmed)
        {
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "User Emain not Confirmed!"
            });
        }
        switch(dto.PersonalType)
        {
            case "Personal":
                try
                {
                    await _personalService.CreatePersonalUserAsync(dto);
                }catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while registering the user");
                    return BadRequest(new
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = ex.Message,
                        ErrorType = "Service Error"
                    });
                }
                break;
            case "Individual":
                try
                {
                    await _invidualService.CreateInvidualUserAsync(dto);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while registering the user");
                    return BadRequest(new
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = ex.Message,
                        ErrorType = "Service Error"
                    });
                }
                break;
            case "Juridical person":
                try
                {
                    await _juridicalService.CreateJuridicalUserAsync(dto);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while registering the user");
                    return BadRequest(new
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = ex.Message,
                        ErrorType = "Service Error"
                    });
                }
                break;
            default:
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "User Type is not valid",
                    ErrorType = ""
                });
        }

        return Ok(new
        {
            StatusCode = StatusCodes.Status200OK,
            UserId = dto.UserId,
        });
    }


    [HttpPost("bank-details")]
    [Authorize]
    public async Task<IActionResult> BankDetails(PersonalBankingInfoDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.UserId);
        if (user is null)
        {
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "User Not Found!"
            });
        }
        PersonalUserDto personalDto;
        try
        {

        personalDto = await _personalService.GetPersonalUserByIdAsync(dto.UserId);
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "User invalid Type",
                
            });
        }

        if (personalDto is null)
        {
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "User Type is not Personal!"
            });
        }

        try
        {
            await _personalService.AddBankingInfoPersonalUserAsync(dto);
        }catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while registering Bank details");
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = ex.Message,
                
            });
        }

        return Ok(new
        {
            StatusCode = StatusCodes.Status200OK
        });
    }


    [HttpPost("bank-det-inv")]
    [Authorize]
    public async Task<IActionResult> InvidualBankDetails(InvidualBankingInfoDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.UserId);
        if (user is null)
        {
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "User Not Found!"
            });
        }
        InvidualUserDto invidualDto;
        try
        {
         invidualDto = await _invidualService.GetInvidualUserByIdAsync(dto.UserId);

        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "User invalid Type",
                
            });
        }


        if (invidualDto is null)
        {
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "User is not found!"
            });
        }
        

        try
        {
            await _invidualService.AddBankingInfoInvidualUserAsync(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while registering Bank details");
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = ex.Message,

            });
        }

        return Ok(new
        {
            StatusCode = StatusCodes.Status200OK
        });
    }

    [HttpPost("bank-det-leg")]
    [Authorize]
    public async Task<IActionResult> JuridicalBankDetails(JuridicalBankingInfoDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.UserId);
        if (user is null)
        {
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "User Not Found!"
            });
        }
        JuridicalUserDto juridicalDto;
        try
        {
            juridicalDto = await _juridicalService.GetJuridicalUserByIdAsync(dto.UserId);

        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "User invalid Type",
                
            });
        }


        if (juridicalDto is null)
        {
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "User is not found!"
            });
        }


        try
        {
            await _juridicalService.AddBankingInfoJuridicalUserAsync(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while registering Bank details");
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                message = ex.Message,

            });
        }

        return Ok(new
        {
            StatusCode = StatusCodes.Status200OK
        });
    }


    [HttpGet("user-data")]
    [Authorize]
    public async Task<IActionResult> GetUserData()
    {
        var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(user is null)
        {
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "User is not Authorize!"
            });
        }
        return Ok(new
        {
            StatusCode = StatusCodes.Status200OK,
            Data = user
        });
    }
}
