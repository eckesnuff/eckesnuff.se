<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="Stormbreaker.Web.Mvc.StormbreakerModelViewPage<GuestBookModel,ContentItem>" %>
<%@ Import Namespace="Stormbreaker.Models"%>
<%@ Import Namespace="EckeSnuff.ViewModels"%>
<%@ Import Namespace="EckeSnuff.Models"%>

<asp:Content ContentPlaceHolderID="ExtraContent" runat="server">
<%
    using (Html.BeginForm("comment", "GuestBook", FormMethod.Post))%>
<% {%>
            <div class="comment-form">
                <%=Html.EditorFor(x => x.CurrentComment)%>
                <div class="editor-button">
                    <input type="submit" value="Comment" />
                </div>
            </div>            
        <%
    }%> 
    <h2>Comments</h2>
    <ul class="entries">
    <%=Html.Partial("CommentList",Model) %>
    </ul>
    <ol class="paging">
    <%for (int i = 1; i <= Math.Ceiling(Model.Comments.Count()/3.0); i++) {%>
            <%if (Model.CurrentPage==i) {%>
                <li class="selected">
            <%}
              else {%>
                <li>
            <%}%>
            <%=Html.ActionLink(i.ToString(), "index", new { pagePath = "gb", page = i })%>
        </li>
    <%}%>
    </ol>
</asp:Content>

