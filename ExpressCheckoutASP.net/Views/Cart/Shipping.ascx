<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<PayPalCodeSample.Models.Shipping>>" %>
<ul>
	<%
	foreach (var item in Model) {
	%>
	<li class="<%=item.Id%>">
		<input id="<%=item.Id%>" type="radio" name="frete" value="<%=item.Id%>" data="<%=item.Value%>" />
		<label for="<%=item.Id%>"><%=item.Name%> <strong>R$ <%=item.Value%></strong></label>
	</li>
	<%
	}
	%>
</ul>