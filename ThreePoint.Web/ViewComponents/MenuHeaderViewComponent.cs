using Microsoft.AspNetCore.Mvc;

namespace ThreePoint.Web.ViewComponents
{
    public class MenuHeaderViewComponent : ViewComponent
    {
        public MenuHeaderViewComponent()
        {

        }

        public IViewComponentResult Invoke()
        {
            //检查是否登录，此部已由控制器完成
            //从检测Session下的RoleId值
            /*var token = request.session["RoleId"];
            if (string.IsNullOrEmpty(token))
            {
                return Content(string.Empty);
            }*/

            /* try
             {
                 var tokenObj = new Token(token);
                 SystemUsers systemUser = AccountRespository.Entity.FirstOrDefault(r => r.Eid.Equals(tokenObj.Eid));
                 if (systemUser == null || !systemUser.IsActive)
                 {
                     return Content(string.Empty);
                 }

                 var menuList = MenuRespository.GetAllRightsMenus(systemUser.Eid, systemUser.MenuRights);
                 ////拼接Menu
                 var html = RenderMenu(menuList);
                 return new HtmlContentViewComponentResult(new HtmlString(html));
             }
             catch (Exception)
             {
                 return Content(string.Empty);
             }*/
            return Content(string.Empty);
            //return Content("我是ViewComponents部件");
        }

        #region Private



        /// <summary>
        /// 绘制Menu
        /// </summary>
        /// <param name="menuList"></param>
        /// <returns></returns>
        /*private string RenderMenu(List<SystemMenuSM> menuList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var mu in menuList)
            {
                //第一层
                sb.AppendLine("<li>");

                if (mu.ChildMunuList.Count == 0)
                {
                    continue;
                    //只有一层
                    //sb.AppendLine(
                    //    "<a class=\"J_menuItem\" href=\"" + (string.IsNullOrEmpty(mu.Url) ? "#" : Url.Content(mu.Url)) + "\"><i class=\"" + mu.Class +
                    //    "\"></i> <span class=\"nav-label\">" + mu.Name + "</span></a>");
                }
                else
                {
                    sb.AppendLine(
                        "<a href=\"#\"><i class=\"" + mu.Class + "\"></i> <span class=\"nav-label\">" + mu.Name +
                        "</span><span class=\"fa arrow\"></span></a>");

                    sb.AppendLine(" <ul class=\"nav nav-second-level collapse\">");
                    foreach (var child2 in mu.ChildMunuList)
                    {
                        if (child2.ChildMunuList.Count == 0)
                        {
                            //增加对固定Url的展示
                            if (!string.IsNullOrEmpty(child2.Url) && child2.Url.ToLower().StartsWith("http"))
                            {
                                sb.AppendLine($" <li><a class=\"J_menuItem\" href=\"" + (string.IsNullOrEmpty(child2.Url) ? "#" : child2.Url) + "\"> <i class=\"" + child2.Class +
                                              "\"></i>" + child2.Name +
                                              "</a></li > ");
                            }
                            else
                            {
                                //只有第二层
                                sb.AppendLine($" <li><a class=\"J_menuItem\" href=\"" + (string.IsNullOrEmpty(child2.Url) ? "#" : Url.Content(child2.Url)) + "\"><i class=\"" + child2.Class +
                                              "\"></i>" + child2.Name +
                                              "</a></li > ");
                            }
                        }
                        else
                        {
                            //有第三层
                            sb.AppendLine("<li>");
                            sb.AppendLine(" <a href=\"#\">" + child2.Name + "<span class=\"fa arrow\"></span></a>");

                            sb.AppendLine(" <ul class=\"nav nav-third-level collapse\">");

                            foreach (var child3 in child2.ChildMunuList)
                            {
                                sb.AppendLine($"<li><a class=\"J_menuItem\" href=\"" + (string.IsNullOrEmpty(child3.Url) ? "#" : Url.Content(child3.Url)) + "\"><i class=\"" + child2.Class +
                                              "\"></i>" + child3.Name +
                                              "</a></li>");
                            }
                            sb.AppendLine("</ul>");
                            sb.AppendLine("</li>");
                        }
                    }

                    sb.AppendLine("</ul>");
                }

                sb.AppendLine("</li>");
            }
            return sb.ToString();
        }*/
        #endregion
    }
}