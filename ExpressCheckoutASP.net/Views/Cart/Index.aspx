<%@ Page Language="C#" MasterPageFile="~/Views/CodeSample.master" Inherits="System.Web.Mvc.ViewPage<PayPalCodeSample.Models.Cart>"%>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
	<div class="main-width">
		<h2>Meu carrinho</h2>
		<table class="cart" border="0" cellpadding="0" cellspacing="0">
			<thead>
				<tr>
					<th class="img"></th>
					<th class="product">Produto</th>
					<th class="qty">Quantidade</th>
					<th class="price">Preço</th>
					<th class="total">Total</th>
				</tr>
			</thead>
			<tbody>
			<%
			foreach (var item in Model.ItemList()) {
			%>
			<tr>
				<td class="img"><img width="50" height="50" alt="<%=Html.Encode(item.Product.Name)%>" src="/Content/image<%=item.Product.Id%>.jpg" /></td>
				<td class="product"><p><%=Html.Encode(item.Product.Name)%><br /><%=Html.Encode(item.Product.Description)%></p></td>
				<td class="qty"><%=item.Quantity%></td>
				<td class="price">R$ <%=item.Product.Price%></td>
				<td class="total">R$ <%=item.Total%></td>
			</tr>
			<%
			}
			%>
			</tbody>
			<tfoot>
				<tr>
					<td colspan="4" rowspan="2">
						<form action="/Cart/Shipping">
							<label for="cep">CEP</label>
							<input type="text" name="cep" id="cep" />
							<input type="submit" value="Calcular" />
						</form>
						<div class="shipping-values"></div>
					</td>
					<td class="total">R$ <%=Model.Total%></td>
				</tr>
			</tfoot>
		</table>
		<table class="pay" border="0" cellpadding="0" cellspacing="0" align="center">
			<tr>
				<td align="center">
					<a id="pay" href="/Cart/Checkout">
						<img  src="https://www.paypal-brasil.com.br/logocenter/util/img/btPagarAgora_1.png" border="0" alt="Imagens de solução" />
					</a>
				</td>
			</tr>
			<tr>
				<td align="left">
					<a href="#" onclick="javascript:window.open('http://www.paypal-brasil.com.br/popupcheckout/','olcwhatispaypal','toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, width=809, height=320');">
						<img  src="https://www.paypal-brasil.com.br/logocenter/util/img/btPagarAgora_2.png" border="0" alt="Imagens de solução" />
					</a>
				</td>
			</tr>
		</table>
	</div>
	<div class="clear"></div>
</asp:Content>