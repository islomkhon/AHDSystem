<?php

/* @UserCountry/adminIndex.twig */
class __TwigTemplate_bbe93bdc28181dd9177a75711c83811149afcf728eea83d72d92bbff353f9f2d extends Twig_Template
{
    public function __construct(Twig_Environment $env)
    {
        parent::__construct($env);

        // line 1
        $this->parent = $this->loadTemplate("admin.twig", "@UserCountry/adminIndex.twig", 1);
        $this->blocks = array(
            'content' => array($this, 'block_content'),
        );
    }

    protected function doGetParent(array $context)
    {
        return "admin.twig";
    }

    protected function doDisplay(array $context, array $blocks = array())
    {
        // line 3
        ob_start();
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_Geolocation")), "html", null, true);
        $context["title"] = ('' === $tmp = ob_get_clean()) ? '' : new Twig_Markup($tmp, $this->env->getCharset());
        // line 1
        $this->parent->display($context, array_merge($this->blocks, $blocks));
    }

    // line 5
    public function block_content($context, array $blocks = array())
    {
        // line 6
        $context["piwik"] = $this->loadTemplate("macros.twig", "@UserCountry/adminIndex.twig", 6);
        // line 7
        echo "
<div piwik-content-intro>
    <h2 piwik-enriched-headline
        help-url=\"https://matomo.org/docs/geo-locate/\"
        id=\"location-providers\">";
        // line 11
        echo \Piwik\piwik_escape_filter($this->env, ($context["title"] ?? $this->getContext($context, "title")), "html", null, true);
        echo "</h2>
    <p>";
        // line 12
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_GeolocationPageDesc")), "html", null, true);
        echo "</p>
</div>
<div piwik-content-block content-title=\"";
        // line 14
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_LocationProvider")), "html_attr");
        echo "\">
<div piwik-location-provider-selection=\"";
        // line 15
        echo \Piwik\piwik_escape_filter($this->env, ($context["currentProviderId"] ?? $this->getContext($context, "currentProviderId")), "html_attr");
        echo "\">

    ";
        // line 17
        if ( !($context["isThereWorkingProvider"] ?? $this->getContext($context, "isThereWorkingProvider"))) {
            // line 18
            echo "        <h3 style=\"margin-top:0;\">";
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_HowToSetupGeoIP")), "html", null, true);
            echo "</h3>
        <p>";
            // line 19
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_HowToSetupGeoIPIntro")), "html", null, true);
            echo "</p>
        <ul style=\"list-style:disc !important;margin-left:2em;\">
            <li style=\"list-style-type: disc !important;\">";
            // line 21
            echo call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_HowToSetupGeoIP_Step1", (("<a rel=\"noreferrer noopener\" href=\"" . ($context["geoLiteUrl"] ?? $this->getContext($context, "geoLiteUrl"))) . "\">"), "</a>", "<a rel=\"noreferrer noopener\" target=\"_blank\" href=\"http://www.maxmind.com/?rId=piwik\">", "</a>"));
            echo "</li>
            <li style=\"list-style-type: disc !important;\">";
            // line 22
            echo call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_HowToSetupGeoIP_Step2", (("'" . ($context["geoLiteFilename"] ?? $this->getContext($context, "geoLiteFilename"))) . "'"), "<strong>", "</strong>"));
            echo "</li>
            <li style=\"list-style-type: disc !important;\">";
            // line 23
            echo call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_HowToSetupGeoIP_Step3", "<strong>", "</strong>", "<span style=\"color:green\"><strong>", "</strong></span>"));
            echo "</li>
            <li style=\"list-style-type: disc !important;\">";
            // line 24
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_HowToSetupGeoIP_Step4")), "html", null, true);
            echo "</li>
        </ul>
        <p>&nbsp;</p>
    ";
        }
        // line 28
        echo "
    <div class=\"row\">
        <div class=\"col s12 push-m9 m3\">";
        // line 30
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_InfoFor", ($context["thisIP"] ?? $this->getContext($context, "thisIP")))), "html", null, true);
        echo "</div>
    </div>

    ";
        // line 33
        $context['_parent'] = $context;
        $context['_seq'] = twig_ensure_traversable(($context["locationProviders"] ?? $this->getContext($context, "locationProviders")));
        foreach ($context['_seq'] as $context["id"] => $context["provider"]) {
            if ($this->getAttribute($context["provider"], "isVisible", array())) {
                // line 34
                echo "    <div class=\"row form-group provider";
                echo \Piwik\piwik_escape_filter($this->env, $context["id"], "html_attr");
                echo "\">
        <div class=\"col s12 m4 l2\">
            <p>
                <input class=\"location-provider\"
                       name=\"location-provider\"
                       value=\"";
                // line 39
                echo \Piwik\piwik_escape_filter($this->env, $context["id"], "html", null, true);
                echo "\"
                       type=\"radio\"
                       ng-model=\"locationSelector.selectedProvider\"
                       id=\"provider_input_";
                // line 42
                echo \Piwik\piwik_escape_filter($this->env, $context["id"], "html", null, true);
                echo "\" ";
                if (($this->getAttribute($context["provider"], "status", array()) != 1)) {
                    echo "disabled=\"disabled\"";
                }
                echo "/>
                <label for=\"provider_input_";
                // line 43
                echo \Piwik\piwik_escape_filter($this->env, $context["id"], "html", null, true);
                echo "\">";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array($this->getAttribute($context["provider"], "title", array()))), "html", null, true);
                echo "</label>
            </p>
            <p class=\"loc-provider-status\">
                ";
                // line 46
                if (($this->getAttribute($context["provider"], "status", array()) == 0)) {
                    // line 47
                    echo "                    <span class=\"is-not-installed\">";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_NotInstalled")), "html", null, true);
                    echo "</span>
                ";
                } elseif (($this->getAttribute(                // line 48
$context["provider"], "status", array()) == 1)) {
                    // line 49
                    echo "                    <span class=\"is-installed\">";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Installed")), "html", null, true);
                    echo "</span>
                ";
                } elseif (($this->getAttribute(                // line 50
$context["provider"], "status", array()) == 2)) {
                    // line 51
                    echo "                    <span class=\"is-broken\">";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Broken")), "html", null, true);
                    echo "</span>
                ";
                }
                // line 53
                echo "            </p>
        </div>
        <div class=\"col s12 m4 l6\">
            <p>";
                // line 56
                echo call_user_func_array($this->env->getFilter('translate')->getCallable(), array($this->getAttribute($context["provider"], "description", array())));
                echo "</p>
            ";
                // line 57
                if ((($this->getAttribute($context["provider"], "status", array()) != 1) && $this->getAttribute($context["provider"], "install_docs", array(), "any", true, true))) {
                    // line 58
                    echo "                <p>";
                    echo $this->getAttribute($context["provider"], "install_docs", array());
                    echo "</p>
            ";
                }
                // line 60
                echo "        </div>
        <div class=\"col s12 m4 l4\">
            ";
                // line 62
                if (($this->getAttribute($context["provider"], "status", array()) == 1)) {
                    // line 63
                    echo "                <div class=\"form-help\">
                    ";
                    // line 64
                    if ((($context["thisIP"] ?? $this->getContext($context, "thisIP")) != "127.0.0.1")) {
                        // line 65
                        echo "                        ";
                        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_CurrentLocationIntro")), "html", null, true);
                        echo ":
                        <div>
                            <br/>
                            <div style=\"position: absolute;\"
                                 piwik-activity-indicator
                                 loading='locationSelector.updateLoading[";
                        // line 70
                        echo \Piwik\piwik_escape_filter($this->env, twig_jsonencode_filter($context["id"]), "html", null, true);
                        echo "]'></div>
                            <span class=\"location\"><strong>";
                        // line 71
                        echo $this->getAttribute($context["provider"], "location", array());
                        echo "</strong></span>
                        </div>
                        <div class=\"text-right\">
                            <a href=\"javascript:;\"
                               ng-click='locationSelector.refreshProviderInfo(";
                        // line 75
                        echo \Piwik\piwik_escape_filter($this->env, twig_jsonencode_filter($context["id"]), "html", null, true);
                        echo ")'>";
                        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Refresh")), "html", null, true);
                        echo "</a>
                        </div>
                    ";
                    } else {
                        // line 78
                        echo "                        ";
                        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_CannotLocalizeLocalIP", ($context["thisIP"] ?? $this->getContext($context, "thisIP")))), "html", null, true);
                        echo "
                    ";
                    }
                    // line 80
                    echo "                </div>
            ";
                }
                // line 82
                echo "            ";
                if (($this->getAttribute($context["provider"], "statusMessage", array(), "any", true, true) && $this->getAttribute($context["provider"], "statusMessage", array()))) {
                    // line 83
                    echo "                <div class=\"form-help\">
                    ";
                    // line 84
                    if (($this->getAttribute($context["provider"], "status", array()) == 2)) {
                        echo "<strong>";
                        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Error")), "html", null, true);
                        echo ":</strong> ";
                    }
                    echo $this->getAttribute($context["provider"], "statusMessage", array());
                    echo "
                </div>
            ";
                }
                // line 87
                echo "            ";
                if (($this->getAttribute($context["provider"], "extra_message", array(), "any", true, true) && $this->getAttribute($context["provider"], "extra_message", array()))) {
                    // line 88
                    echo "                <div class=\"form-help\">
                    ";
                    // line 89
                    echo $this->getAttribute($context["provider"], "extra_message", array());
                    echo "
                </div>
            ";
                }
                // line 92
                echo "        </div>
    </div>
    ";
            }
        }
        $_parent = $context['_parent'];
        unset($context['_seq'], $context['_iterated'], $context['id'], $context['provider'], $context['_parent'], $context['loop']);
        $context = array_intersect_key($context, $_parent) + $_parent;
        // line 95
        echo "
    <div piwik-save-button onconfirm=\"locationSelector.save()\" saving=\"locationSelector.isLoading\"></div>

</div>
</div>

";
        // line 101
        if ((((isset($context["geoIPLegacyLocUrl"]) || array_key_exists("geoIPLegacyLocUrl", $context)) && ($context["geoIPLegacyLocUrl"] ?? $this->getContext($context, "geoIPLegacyLocUrl"))) && ($context["isInternetEnabled"] ?? $this->getContext($context, "isInternetEnabled")))) {
            // line 102
            echo "    ";
            // line 103
            echo "    <div piwik-content-block content-title=\"Automatic Updates for GeoIP Legacy\">

        <p>Setting up automatic updates for GeoIP Legacy is no longer supported.</p>

        <div class=\"notification system notification-warning\">
            ";
            // line 108
            if (twig_in_filter("GeoLite", ($context["geoIPLegacyLocUrl"] ?? $this->getContext($context, "geoIPLegacyLocUrl")))) {
                // line 109
                echo "                <div>Maxmind announced to discontinue updates to the GeoLite Legacy databases as of April 1, 2018.</div>
            ";
            }
            // line 111
            echo "            <strong>Please consider switching to GeoIP 2 soon! GeoIP Legacy Support is deprecated and will be removed in one of the next major releases.</strong>
        </div>

        ";
            // line 114
            if (((($context["geoIPLegacyLocUrl"] ?? $this->getContext($context, "geoIPLegacyLocUrl")) || ($context["geoIPLegacyIspUrl"] ?? $this->getContext($context, "geoIPLegacyIspUrl"))) || ($context["geoIPLegacyOrgUrl"] ?? $this->getContext($context, "geoIPLegacyOrgUrl")))) {
                // line 115
                echo "            <h3>GeoIP Legacy Auto Update</h3>

            <p>Your previous configuration for automatic updates for GeoIP legacy databases is still up and running. It will be automatically disabled and removed after switching to GeoIP2.</p>

            <p>Below you can find the current configuration:</p>

            ";
                // line 121
                if (($context["geoIPLegacyLocUrl"] ?? $this->getContext($context, "geoIPLegacyLocUrl"))) {
                    echo "<p>";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_LocationDatabase")), "html_attr");
                    echo ": ";
                    echo \Piwik\piwik_escape_filter($this->env, ($context["geoIPLegacyLocUrl"] ?? $this->getContext($context, "geoIPLegacyLocUrl")), "html", null, true);
                    echo "</p>";
                }
                // line 122
                echo "            ";
                if (($context["geoIPLegacyIspUrl"] ?? $this->getContext($context, "geoIPLegacyIspUrl"))) {
                    echo "<p>";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_ISPDatabase")), "html_attr");
                    echo ": ";
                    echo \Piwik\piwik_escape_filter($this->env, ($context["geoIPLegacyIspUrl"] ?? $this->getContext($context, "geoIPLegacyIspUrl")), "html", null, true);
                    echo "</p>";
                }
                // line 123
                echo "            ";
                if (($context["geoIPLegacyOrgUrl"] ?? $this->getContext($context, "geoIPLegacyOrgUrl"))) {
                    echo "<p>";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_OrgDatabase")), "html_attr");
                    echo ": ";
                    echo \Piwik\piwik_escape_filter($this->env, ($context["geoIPLegacyOrgUrl"] ?? $this->getContext($context, "geoIPLegacyOrgUrl")), "html", null, true);
                    echo "</p>";
                }
                // line 124
                echo "            ";
                if (($context["geoIPLegacyUpdatePeriod"] ?? $this->getContext($context, "geoIPLegacyUpdatePeriod"))) {
                    echo "<p>";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_DownloadNewDatabasesEvery")), "html_attr");
                    echo ": ";
                    echo \Piwik\piwik_escape_filter($this->env, ($context["geoIPLegacyUpdatePeriod"] ?? $this->getContext($context, "geoIPLegacyUpdatePeriod")), "html", null, true);
                    echo "</p>";
                }
                // line 125
                echo "
        ";
            }
            // line 127
            echo "    </div>
";
        }
        // line 129
        echo "
";
        // line 130
        if (($context["isInternetEnabled"] ?? $this->getContext($context, "isInternetEnabled"))) {
            // line 131
            echo "    <div piwik-content-block
         content-title=\"";
            // line 132
            if ( !($context["geoIPDatabasesInstalled"] ?? $this->getContext($context, "geoIPDatabasesInstalled"))) {
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_GeoIPDatabases")), "html_attr");
            } else {
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_SetupAutomaticUpdatesOfGeoIP")), "html_attr");
            }
            echo "\"
         id=\"geoip-db-mangement\">

        <div piwik-location-provider-updater
             geoip-database-installed=\"";
            // line 136
            if (($context["geoIPDatabasesInstalled"] ?? $this->getContext($context, "geoIPDatabasesInstalled"))) {
                echo "1";
            } else {
                echo "0";
            }
            echo "\">

            ";
            // line 138
            if (($context["showGeoIPUpdateSection"] ?? $this->getContext($context, "showGeoIPUpdateSection"))) {
                // line 139
                echo "                ";
                if ( !($context["geoIPDatabasesInstalled"] ?? $this->getContext($context, "geoIPDatabasesInstalled"))) {
                    // line 140
                    echo "                    <div ng-show=\"!locationUpdater.geoipDatabaseInstalled\">
                        <div ng-show=\"locationUpdater.showPiwikNotManagingInfo\">
                            <h3>";
                    // line 142
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_PiwikNotManagingGeoIPDBs")), "html_attr");
                    echo "</h3>
                            <div id=\"manage-geoip-dbs\">
                                <div class=\"row\" id=\"geoipdb-screen1\">
                                    <div class=\"geoipdb-column-1 col s6\">
                                        <p>";
                    // line 146
                    echo call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_IWantToDownloadFreeGeoIP"));
                    echo "</p>
                                    </div>
                                    <div class=\"geoipdb-column-2 col s6\">
                                        <p>";
                    // line 149
                    echo call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_IPurchasedGeoIPDBs", "<a href=\"http://www.maxmind.com/en/geolocation_landing?rId=piwik\">", "</a>"));
                    echo "</p>
                                    </div>
                                    <div class=\"geoipdb-column-1 col s6\">
                                        <input type=\"button\" class=\"btn\"
                                               ng-click=\"locationUpdater.startDownloadFreeGeoIp()\"
                                               value=\"";
                    // line 154
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_GetStarted")), "html", null, true);
                    echo "...\"/>
                                    </div>
                                    <div class=\"geoipdb-column-2 col s6\">
                                        <input type=\"button\" class=\"btn\"
                                               ng-click=\"locationUpdater.startAutomaticUpdateGeoIp()\"
                                               value=\"";
                    // line 159
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_GetStarted")), "html", null, true);
                    echo "...\" id=\"start-automatic-update-geoip\"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id=\"geoipdb-screen2-download\" ng-show=\"locationUpdater.showFreeDownload\">
                            <div piwik-progressbar
                                 label=\"";
                    // line 166
                    echo \Piwik\piwik_escape_filter($this->env, twig_jsonencode_filter((call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_DownloadingDb", (((("<a href=\"" . ($context["geoLiteUrl"] ?? $this->getContext($context, "geoLiteUrl"))) . "\">") . ($context["geoLiteFilename"] ?? $this->getContext($context, "geoLiteFilename"))) . "</a>"))) . "...")), "html", null, true);
                    echo "\"
                                 progress=\"locationUpdater.progressFreeDownload\">
                            </div>
                        </div>
                    </div>
                ";
                }
                // line 172
                echo "
                ";
                // line 173
                $this->loadTemplate("@UserCountry/_updaterManage.twig", "@UserCountry/adminIndex.twig", 173)->display($context);
                // line 174
                echo "            ";
            } else {
                // line 175
                echo "                <p class=\"form-description\">";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("UserCountry_CannotSetupGeoIPAutoUpdating")), "html", null, true);
                echo "</p>
            ";
            }
            // line 177
            echo "        </div>
    </div>
";
        }
        // line 180
        echo "
";
    }

    public function getTemplateName()
    {
        return "@UserCountry/adminIndex.twig";
    }

    public function isTraitable()
    {
        return false;
    }

    public function getDebugInfo()
    {
        return array (  452 => 180,  447 => 177,  441 => 175,  438 => 174,  436 => 173,  433 => 172,  424 => 166,  414 => 159,  406 => 154,  398 => 149,  392 => 146,  385 => 142,  381 => 140,  378 => 139,  376 => 138,  367 => 136,  356 => 132,  353 => 131,  351 => 130,  348 => 129,  344 => 127,  340 => 125,  331 => 124,  322 => 123,  313 => 122,  305 => 121,  297 => 115,  295 => 114,  290 => 111,  286 => 109,  284 => 108,  277 => 103,  275 => 102,  273 => 101,  265 => 95,  256 => 92,  250 => 89,  247 => 88,  244 => 87,  233 => 84,  230 => 83,  227 => 82,  223 => 80,  217 => 78,  209 => 75,  202 => 71,  198 => 70,  189 => 65,  187 => 64,  184 => 63,  182 => 62,  178 => 60,  172 => 58,  170 => 57,  166 => 56,  161 => 53,  155 => 51,  153 => 50,  148 => 49,  146 => 48,  141 => 47,  139 => 46,  131 => 43,  123 => 42,  117 => 39,  108 => 34,  103 => 33,  97 => 30,  93 => 28,  86 => 24,  82 => 23,  78 => 22,  74 => 21,  69 => 19,  64 => 18,  62 => 17,  57 => 15,  53 => 14,  48 => 12,  44 => 11,  38 => 7,  36 => 6,  33 => 5,  29 => 1,  25 => 3,  11 => 1,);
    }

    /** @deprecated since 1.27 (to be removed in 2.0). Use getSourceContext() instead */
    public function getSource()
    {
        @trigger_error('The '.__METHOD__.' method is deprecated since version 1.27 and will be removed in 2.0. Use getSourceContext() instead.', E_USER_DEPRECATED);

        return $this->getSourceContext()->getCode();
    }

    public function getSourceContext()
    {
        return new Twig_Source("{% extends 'admin.twig' %}

{% set title %}{{ 'UserCountry_Geolocation'|translate }}{% endset %}

{% block content %}
{% import 'macros.twig' as piwik %}

<div piwik-content-intro>
    <h2 piwik-enriched-headline
        help-url=\"https://matomo.org/docs/geo-locate/\"
        id=\"location-providers\">{{ title }}</h2>
    <p>{{ 'UserCountry_GeolocationPageDesc'|translate }}</p>
</div>
<div piwik-content-block content-title=\"{{ 'UserCountry_LocationProvider'|translate|e('html_attr') }}\">
<div piwik-location-provider-selection=\"{{ currentProviderId|e('html_attr') }}\">

    {% if not isThereWorkingProvider %}
        <h3 style=\"margin-top:0;\">{{ 'UserCountry_HowToSetupGeoIP'|translate }}</h3>
        <p>{{ 'UserCountry_HowToSetupGeoIPIntro'|translate }}</p>
        <ul style=\"list-style:disc !important;margin-left:2em;\">
            <li style=\"list-style-type: disc !important;\">{{ 'UserCountry_HowToSetupGeoIP_Step1'|translate('<a rel=\"noreferrer noopener\" href=\"'~geoLiteUrl~'\">','</a>','<a rel=\"noreferrer noopener\" target=\"_blank\" href=\"http://www.maxmind.com/?rId=piwik\">','</a>')|raw }}</li>
            <li style=\"list-style-type: disc !important;\">{{ 'UserCountry_HowToSetupGeoIP_Step2'|translate(\"'\"~geoLiteFilename~\"'\",'<strong>','</strong>')|raw }}</li>
            <li style=\"list-style-type: disc !important;\">{{ 'UserCountry_HowToSetupGeoIP_Step3'|translate('<strong>','</strong>','<span style=\"color:green\"><strong>','</strong></span>')|raw }}</li>
            <li style=\"list-style-type: disc !important;\">{{ 'UserCountry_HowToSetupGeoIP_Step4'|translate }}</li>
        </ul>
        <p>&nbsp;</p>
    {% endif %}

    <div class=\"row\">
        <div class=\"col s12 push-m9 m3\">{{ 'General_InfoFor'|translate(thisIP) }}</div>
    </div>

    {% for id,provider in locationProviders if provider.isVisible %}
    <div class=\"row form-group provider{{ id|e('html_attr') }}\">
        <div class=\"col s12 m4 l2\">
            <p>
                <input class=\"location-provider\"
                       name=\"location-provider\"
                       value=\"{{ id }}\"
                       type=\"radio\"
                       ng-model=\"locationSelector.selectedProvider\"
                       id=\"provider_input_{{ id }}\" {% if provider.status != 1 %}disabled=\"disabled\"{% endif %}/>
                <label for=\"provider_input_{{ id }}\">{{ provider.title|translate }}</label>
            </p>
            <p class=\"loc-provider-status\">
                {% if provider.status == 0 %}
                    <span class=\"is-not-installed\">{{ 'General_NotInstalled'|translate}}</span>
                {% elseif provider.status == 1 %}
                    <span class=\"is-installed\">{{ 'General_Installed'|translate }}</span>
                {% elseif provider.status == 2 %}
                    <span class=\"is-broken\">{{ 'General_Broken'|translate }}</span>
                {% endif %}
            </p>
        </div>
        <div class=\"col s12 m4 l6\">
            <p>{{ provider.description|translate|raw }}</p>
            {% if provider.status != 1 and provider.install_docs is defined %}
                <p>{{ provider.install_docs|raw }}</p>
            {% endif %}
        </div>
        <div class=\"col s12 m4 l4\">
            {% if provider.status == 1 %}
                <div class=\"form-help\">
                    {% if thisIP != '127.0.0.1' %}
                        {{ 'UserCountry_CurrentLocationIntro'|translate }}:
                        <div>
                            <br/>
                            <div style=\"position: absolute;\"
                                 piwik-activity-indicator
                                 loading='locationSelector.updateLoading[{{ id|json_encode }}]'></div>
                            <span class=\"location\"><strong>{{ provider.location|raw }}</strong></span>
                        </div>
                        <div class=\"text-right\">
                            <a href=\"javascript:;\"
                               ng-click='locationSelector.refreshProviderInfo({{ id|json_encode }})'>{{ 'General_Refresh'|translate }}</a>
                        </div>
                    {% else %}
                        {{ 'UserCountry_CannotLocalizeLocalIP'|translate(thisIP) }}
                    {% endif %}
                </div>
            {% endif %}
            {% if provider.statusMessage is defined and provider.statusMessage %}
                <div class=\"form-help\">
                    {% if provider.status == 2 %}<strong>{{ 'General_Error'|translate }}:</strong> {% endif %}{{ provider.statusMessage|raw }}
                </div>
            {% endif %}
            {% if provider.extra_message is defined and provider.extra_message %}
                <div class=\"form-help\">
                    {{ provider.extra_message|raw }}
                </div>
            {% endif %}
        </div>
    </div>
    {% endfor %}

    <div piwik-save-button onconfirm=\"locationSelector.save()\" saving=\"locationSelector.isLoading\"></div>

</div>
</div>

{% if geoIPLegacyLocUrl is defined and geoIPLegacyLocUrl and isInternetEnabled %}
    {# The text in this part is not translatable on purpose, as it will be removed again soon #}
    <div piwik-content-block content-title=\"Automatic Updates for GeoIP Legacy\">

        <p>Setting up automatic updates for GeoIP Legacy is no longer supported.</p>

        <div class=\"notification system notification-warning\">
            {% if 'GeoLite' in geoIPLegacyLocUrl %}
                <div>Maxmind announced to discontinue updates to the GeoLite Legacy databases as of April 1, 2018.</div>
            {% endif %}
            <strong>Please consider switching to GeoIP 2 soon! GeoIP Legacy Support is deprecated and will be removed in one of the next major releases.</strong>
        </div>

        {% if geoIPLegacyLocUrl or geoIPLegacyIspUrl or geoIPLegacyOrgUrl %}
            <h3>GeoIP Legacy Auto Update</h3>

            <p>Your previous configuration for automatic updates for GeoIP legacy databases is still up and running. It will be automatically disabled and removed after switching to GeoIP2.</p>

            <p>Below you can find the current configuration:</p>

            {% if geoIPLegacyLocUrl %}<p>{{ 'UserCountry_LocationDatabase'|translate|e('html_attr') }}: {{ geoIPLegacyLocUrl }}</p>{% endif %}
            {% if geoIPLegacyIspUrl %}<p>{{ 'UserCountry_ISPDatabase'|translate|e('html_attr') }}: {{ geoIPLegacyIspUrl }}</p>{% endif %}
            {% if geoIPLegacyOrgUrl %}<p>{{ 'UserCountry_OrgDatabase'|translate|e('html_attr') }}: {{ geoIPLegacyOrgUrl }}</p>{% endif %}
            {% if geoIPLegacyUpdatePeriod %}<p>{{ 'UserCountry_DownloadNewDatabasesEvery'|translate|e('html_attr') }}: {{ geoIPLegacyUpdatePeriod }}</p>{% endif %}

        {% endif %}
    </div>
{% endif %}

{% if isInternetEnabled %}
    <div piwik-content-block
         content-title=\"{% if not geoIPDatabasesInstalled %}{{ 'UserCountry_GeoIPDatabases'|translate|e('html_attr') }}{% else %}{{ 'UserCountry_SetupAutomaticUpdatesOfGeoIP'|translate|e('html_attr') }}{% endif %}\"
         id=\"geoip-db-mangement\">

        <div piwik-location-provider-updater
             geoip-database-installed=\"{% if geoIPDatabasesInstalled %}1{% else %}0{% endif %}\">

            {% if showGeoIPUpdateSection %}
                {% if not geoIPDatabasesInstalled %}
                    <div ng-show=\"!locationUpdater.geoipDatabaseInstalled\">
                        <div ng-show=\"locationUpdater.showPiwikNotManagingInfo\">
                            <h3>{{ 'UserCountry_PiwikNotManagingGeoIPDBs'|translate|e('html_attr') }}</h3>
                            <div id=\"manage-geoip-dbs\">
                                <div class=\"row\" id=\"geoipdb-screen1\">
                                    <div class=\"geoipdb-column-1 col s6\">
                                        <p>{{ 'UserCountry_IWantToDownloadFreeGeoIP'|translate|raw }}</p>
                                    </div>
                                    <div class=\"geoipdb-column-2 col s6\">
                                        <p>{{ 'UserCountry_IPurchasedGeoIPDBs'|translate('<a href=\"http://www.maxmind.com/en/geolocation_landing?rId=piwik\">','</a>')|raw }}</p>
                                    </div>
                                    <div class=\"geoipdb-column-1 col s6\">
                                        <input type=\"button\" class=\"btn\"
                                               ng-click=\"locationUpdater.startDownloadFreeGeoIp()\"
                                               value=\"{{ 'General_GetStarted'|translate }}...\"/>
                                    </div>
                                    <div class=\"geoipdb-column-2 col s6\">
                                        <input type=\"button\" class=\"btn\"
                                               ng-click=\"locationUpdater.startAutomaticUpdateGeoIp()\"
                                               value=\"{{ 'General_GetStarted'|translate }}...\" id=\"start-automatic-update-geoip\"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id=\"geoipdb-screen2-download\" ng-show=\"locationUpdater.showFreeDownload\">
                            <div piwik-progressbar
                                 label=\"{{ ('UserCountry_DownloadingDb'|translate('<a href=\"'~geoLiteUrl~'\">'~geoLiteFilename~'</a>') ~ '...')|json_encode }}\"
                                 progress=\"locationUpdater.progressFreeDownload\">
                            </div>
                        </div>
                    </div>
                {% endif %}

                {% include \"@UserCountry/_updaterManage.twig\" %}
            {% else %}
                <p class=\"form-description\">{{ 'UserCountry_CannotSetupGeoIPAutoUpdating'|translate }}</p>
            {% endif %}
        </div>
    </div>
{% endif %}

{% endblock %}

", "@UserCountry/adminIndex.twig", "D:\\xampp\\htdocs\\matomo\\plugins\\UserCountry\\templates\\adminIndex.twig");
    }
}
