(function (window, document, undefined) {

    var M = {
        version: '0.1',
        assert: false, // commented production code contains 'if (M.assert'
        debug: false
    };

    function expose() {
        var oldM = window.M;

        M.noConflict = function () {
            window.M = oldM;
            return this;
        };

        window.M = M;
    }

    // define M as a global M variable, saving the original M to restore later if needed
    if (typeof window !== 'undefined') {
        expose();
    }


    M.Util = {
        isNullOrUndefined: function (x) { return (x === null) || (x === undefined); },
        isNullUndefinedOrEmpty: function (x) { return ((x === null) || (x === undefined) || !x.length); },
    };

    M.isNullOrUndefined = M.Util.isNullOrUndefined;
    M.isNullUndefinedOrEmpty = M.Util.isNullUndefinedOrEmpty;


    M.Enum = {
        Color: {
            Red: 1,
            Blue: 2,
            Green: 3
        },
        Size: {
            Small: 1,
            Medium: 2,
            Large: 3
        },
        ObjectType: {
            Product: "Product",
            Me: "Me"
        },
    };

    M.Color = M.Enum.Color;
    M.Size = M.Enum.Size;
    M.ObjectType = M.Enum.ObjectType;


    M.Validators = {
        isFirstSubmit: function ($element) {
            var isSubmitted = $element.data("submitted");
            if (!isSubmitted) {
                $element.data("submitted", true);
                return true;
            }
            return false;
        }
    };

    M.isFirstSubmit = M.Validators.isFirstSubmit;

    M.Modal = {};

    M.Page = {};

    M.Control = {};

}(window, document));

function chr(num) {
    return String.fromCharCode(num);
};