//IIFE
M.Product = (function () {


    var _product = function (data) {
        if (M.isNullOrUndefined(data))
            return;

        this.Id = data.Id || '';
        this.Name = data.Name || '';
        this.Size = data.Size || '';
        this.Quantity = data.Quantity || '';
        this.Price = data.Price || '';
        this.Color = data.Color || '';
        this.Source = data.Source || '';
        this.Object = M.ObjectType.Product;


    };

    _product.Update = function (id, item) {
        var d = $.Deferred();
        $.ajax({
            url: 'api/v1/products/' + id,
            type: 'PUT',
            data: JSON.stringify(item),
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                d.resolve(data);
            },
            fail: function (response) {
                d.reject(response);
            }
        });
        return d.promise();
    };

    _product.RemoveFromCart = function (id) {
        var d = $.Deferred();
        $.ajax({
            url: 'api/v1/products/' + id,
            type: 'DELETE',
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                d.resolve(data);
            },
            fail: function (response) {
                d.reject(response);
            }
        });
        return d.promise();
    };

    _product.Save = function (userId, data) {
        var d = $.Deferred();
        $.ajax({
            url: 'api/v1/products/' + userId,
            type: 'POST',
            data: JSON.stringify(data),
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                d.resolve(data);
            },
            fail: function (response) {
                d.reject(response);
            }
        });
        return d.promise();
    };

    _product.AddToCart = function (data) {
        var d = $.Deferred();
        $.ajax({
            url: 'api/v1/products/',
            type: 'POST',
            data: JSON.stringify(data),
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                d.resolve(data);
            },
            fail: function (response) {
                d.reject(response);
            }
        });
        return d.promise();
    };

    _product.Get = function (userId) {
        var d = $.Deferred();
        $.ajax({
            url: 'api/v1/products/' + userId,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                d.resolve(data);
            },
            fail: function (response) {
                d.reject(response);
            }
        });
        return d.promise();
    };

    return _product;

})();