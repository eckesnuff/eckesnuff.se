<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<FlickrItem>>" %>
<%@ Import Namespace="EckeSnuff.Web.Mvc.Html"%>
<%@ Import Namespace="EckeSnuff.Entities"%>
<%=Html.FeedMe(Model,12)%>
