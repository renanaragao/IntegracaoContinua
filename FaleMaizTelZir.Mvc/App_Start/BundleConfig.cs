﻿using System.Web;
using System.Web.Optimization;

namespace FaleMaizTelZir.Mvc
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/priceformat").Include(
                        "~/Scripts/jquery.price_format.2.0.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/global.js",
                "~/Scripts/angular.min.js",
                "~/Angular/Chamada/appChamada.js",
                "~/Angular/Chamada/Service/ChamadaService.js",
                "~/Angular/Chamada/Controller/ChamadaController.js",
                "~/Angular/FaleMaisTelZir/appFaleMaisTelZir.js",
                "~/Angular/FaleMaisTelZir/Directiva/Number.js",
                "~/Angular/FaleMaisTelZir/Directiva/Money.js"
                ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
