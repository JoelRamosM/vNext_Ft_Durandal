requirejs.config({
    paths: {
        'text': "Assets/scripts/vendor/text",
        'durandal': "Assets/scripts/vendor/durandal",
        'plugins': "Assets/scripts/vendor/durandal/plugins",
        'transitions': "Assets/scripts/vendor/durandal/transitions"
    }
});

define("jquery", function () { return jQuery; });
define("knockout", ko);

define(["durandal/system", "durandal/app", "durandal/viewLocator"], function (system, app, viewLocator) {
    //>>excludeStart("build", true);
    system.debug(false);
    //>>excludeEnd("build");

    app.title = "Asp.Net 5 - Durandal";

    app.configurePlugins({
        router: true,
        dialog: true
    });

    app.start().then(function () {
        //Replace 'viewmodels' in the moduleId with 'views' to locate the view.
        //Look for partial views in a 'views' folder in the root.
        viewLocator.useConvention();
        //viewLocator.useConvention('', 'views/home');
        //Show the app by setting the root view model for our application with a transition.
        app.setRoot("viewmodels/home/shell", "entrance");
    });
});