<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<ContentItem>>" %>
<%@ Import Namespace="EckeSnuff.ViewModels"%>
<%@ Import Namespace="EckeSnuff.Web.Mvc.Html"%>
<%@ Import Namespace="Stormbreaker.Models"%>
<%=Html.Navigation("id=\"nav\"",Model) %>

