<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<RssItem>>" %>
<%@ Import Namespace="EckeSnuff.Web.Mvc.Html"%>
<%@ Import Namespace="EckeSnuff.Entities"%>
<%@ Import Namespace="EckeSnuff.Web"%>
<%=Html.FeedMe(Model,8)%>