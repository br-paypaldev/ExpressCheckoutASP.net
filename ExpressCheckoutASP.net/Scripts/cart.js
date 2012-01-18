$(function() {
	var shipping = null;
	var cep = null;
	
	$('.shipping-values').hide();
	$('tfoot form').submit(function( e ) {
		var _cep = $('#cep' ).val();
		
		e.preventDefault();
		
		if ( _cep.length != "" ) {
			cep = _cep;
			
			$.ajax( {
				url : "/Cart/Shipping?cep=" + cep,
				success : function( r ) {
					$('.shipping-values').show();
					$('.shipping-values').replaceWith( '<div class="shipping-values">' + r + '</div>' );
					$('.shipping-values input[type="radio"]').change(function(){
						var total = parseFloat($(this).attr('data').split(',').join('.'));
						shipping = $(this).val();
						
						$('tbody td.total').each(function(){
							var itemTotal = parseFloat(
								$(this).text().split( 'R$ ' ).join('').split(',').join('.')
							);
							
							total += itemTotal;
						});
						
						total = Math.round(total*100)/100;
						
						$('tfoot .total').html('R$ ' + total );
					});
				}
			} );
		}
	});
	
	$('#pay').click(function(e) {
		var href=$(this).attr('href');
		
		e.preventDefault();
		
		if ( shipping == null ) {
			alert( 'Você precisa calcular o frete' );
		} else {
			window.location.href=href + '?cep=' + cep + '&type=' + shipping;
		}
	});
});
