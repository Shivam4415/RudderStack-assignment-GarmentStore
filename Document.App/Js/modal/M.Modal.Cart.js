M.Modal.Cart = (function () {

    var _product = [];
    var _userId;
    var _show = function (data) {
        if (M.isNullUndefinedOrEmpty(data))
        {
            UIkit.notification({
                message: 'Add some product to your cart to proceed with checkout',
                status: 'primary',
                timeout: '3000',
                pos: 'top-center'
            });

            return;
        }
            
        _userId = meObject.value;
        _product = [];
        var header = '<tr class="uk-width-1-1"><th class="uk-width-1-5">Name</th><th class="uk-width-1-5">Price</th><th class="uk-width-1-5">Quantity</th><th class="uk-width-1-5">Color</th><th class="uk-width-1-5">Size</th></tr>';
        var row = '';
        var price = 0;
        
        $("#productsList").empty();
        data.forEach(d => {
            var size, color;

            Object.keys(M.Size).forEach(
                key => {
                    if(M.Size[key] == d.Product.Size[0])
                    {
                        size = key;
                    }
                } 
            );

            Object.keys(M.Color).forEach(
                key => {
                    if (M.Color[key] == d.Product.Color[0]) {
                        color = key;
                    }
                }
            );

            row += '<tr><td class="uk-width-1-5 uk-text-center">' + d.Product.Name + '</td><td class="uk-width-1-5 uk-text-center" >' + 'Rs.' + d.Product.Price.Value / 100 + '</td><td class="uk-width-1-5 uk-text-center">' + d.Product.Quantity + '</td><td class="uk-width-1-5 uk-text-center">' + color + '</td><td class="uk-width-1-5 uk-text-center">' + size + '</td></tr>';
            price += parseInt(d.Product.Price.Value);
            _product.push(d.Product);
        });
        $("#productsList").append(header + row);
        $("#totalPrice").text('Rs.' + price/100);
        UIkit.modal($("#cartModal")).show();

        //$("#saveButton").on('click', _orderProduct);
    };

    //var _orderProduct = function () {
    //    M.Product.Save(_userId, _product).done(function (response) {
    //        M.Page.Product.cart = [];
    //        M.Page.Product.cartCount = 0;
    //    });
        
    //};

    return {
        show: _show
    };

})();