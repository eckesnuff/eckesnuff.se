<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GuestBookModel>" %>
<%@ Import Namespace="EckeSnuff.ViewModels"%>
<%@ Import Namespace="EckeSnuff.Models"%>
<%foreach (var comment in Model.Comments.OrderByDescending(x => x.StartPublish).Skip((Model.CurrentPage - 1)*3).Take(3)) {%>
        <li>
            <div class="meta"><span class="name"><%=comment.Author%></span><span class="time"><%=comment.StartPublish.Value.ToString("yyyy-MM-dd HH:mm:ss")%></span></div>
            <p><%=comment.CommentContent%></p>
         </li>
    <%}%>