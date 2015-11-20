/// <binding AfterBuild='default' />
//TODO: https://www.npmjs.com/package/gulp-main-bower-files
var gulp = require("gulp");
var util = require("gulp-util");
var rename = require("gulp-rename");
var bower = require("gulp-bower");
var watch = require("gulp-watch");
var cssmin = require("gulp-cssmin");
var stripCssComments = require("gulp-strip-css-comments");
var concat = require("gulp-concat");

var browserify = require("browserify");
var source = require("vinyl-source-stream");
var stringify = require("stringify");

var stylus = require("gulp-stylus");


var app_stylus_base = "wwwroot/Assets/styles/app/app.styl";

var vendor_script_dest = "wwwroot/Assets/scripts/vendor";
var vendor_style_dest = "wwwroot/Assets/styles/vendor";

var app_style_dest = "wwwroot/Assets/styles";

var vendors_scripts_files_srcs = ["bower_components/knockout/dist/knockout.js",
                                    "bower_components/jquery/dist/jquery.min.js",
                                    "bower_components/jqueryui-datepicker/datepicker.js",
                                    "bower_components/jqueryui-datepicker/core.js",
                                    "bower_components/jqueryui-datepicker/i18n/datepicker-pt-BR.js",
                                    "bower_components/bootstrap/dist/js/bootstrap.min.js",
                                    "bower_components/requirejs/require.js",
                                    "bower_components/requirejs-text/text.js",
                                    "bower_components/moment/min/moment.min.js",
                                    "bower_components/toastr/toastr.js"

];

var vendors_styles_files_srcs = [
        "bower_components/bootstrap/dist/css/bootstrap.min.css",
        "bower_components/bootstrap/dist/css/bootstrap-theme.min.css",
        "bower_components/durandal/css/durandal.css",
        "bower_components/toastr/toastr.min.css"


];

var vendors_fonts_files_srcs = ["bower_components/bootstrap/dist/fonts/**"];


gulp.task("default", ["bower", "bundle-vendor", "bundle-app", "build-stylus"], function () {

});

gulp.task("bower", function () {
    return bower();
});

gulp.task("bundle-vendor", function () {

    installDurandalFiles();
    prepareVendorStyles();
    return gulp.src(vendors_scripts_files_srcs)
            .pipe(gulp.dest(vendor_script_dest))
            .on("end", bundleVendorScripts);
});

gulp.task("bundle-app", function () {
    util.log(util.colors.green("STARTING BUNDLING APP SCRIPTS...."));
    return browserify({ entries: ["wwwroot/Assets/scripts/app/all.js"] })
                        .transform(stringify([".html"]))
                        .bundle()
                        .pipe(source("app.bundled.js"))
                        .pipe(gulp.dest("wwwroot/Assets/scripts"))
                        .on("end", function () { util.log(util.colors.green("APP SCRIPTS BUNDLED!")); });
});

gulp.task("build-stylus", function () {
    util.log(util.colors.green("Starting to build stylus..."));
    gulp.src(app_stylus_base)
        .pipe(stylus())
        .pipe(gulp.dest(app_style_dest))
        .on("end", function () { util.log(util.colors.green("All Stylus files are builded...")); });
});


function installDurandalFiles() {
    gulp.src(["bower_components/durandal/js/**"])
       .pipe(gulp.dest(vendor_script_dest + "/durandal"));
}

function prepareVendorStyles() {
    gulp.src(vendors_styles_files_srcs)
                   .pipe(concat("vendor.min.css"))
                   .pipe(stripCssComments({ all: true }))
                   .pipe(cssmin())
                   .pipe(gulp.dest(vendor_style_dest + "/css"));
    return prepareVendorFonts();
};

function prepareVendorFonts() {
    return gulp.src(vendors_fonts_files_srcs)
        .pipe(gulp.dest(vendor_style_dest + "/fonts"));
};

function bundleVendorScripts() {
    util.log(util.colors.green("STARTING BUNDLING VENDOR SCRIPTS...."));

    return browserify({ entries: ["wwwroot/Assets/scripts/vendor/all.js"], noParse: ["./bower_components/jqueri-ui/ui/minified/datepicker.min.js"] })
                        .require("./wwwroot/Assets/scripts/vendor/jquery.min.js", { expose: "jquery" })
                        .require("./wwwroot/Assets/scripts/vendor/moment.min.js", { expose: "moment" })
                        .bundle()
                        .pipe(source("vendor.bundled.js"))
                        .pipe(gulp.dest("wwwroot/Assets/scripts"))
                        .on("end", function () { util.log(util.colors.green("VENDOR SCRIPTS BUNDLED!")); });
}
