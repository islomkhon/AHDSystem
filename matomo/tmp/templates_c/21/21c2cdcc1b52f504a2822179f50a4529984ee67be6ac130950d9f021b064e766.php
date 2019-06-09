<?php

/* @Marketplace/licenseform.twig */
class __TwigTemplate_2170e6ad50df0318c6e2e064145394c5c3f34ef7eb8212884bd6d61646fe7758 extends Twig_Template
{
    public function __construct(Twig_Environment $env)
    {
        parent::__construct($env);

        $this->parent = false;

        $this->blocks = array(
        );
    }

    protected function doDisplay(array $context, array $blocks = array())
    {
        // line 1
        ob_start();
        // line 2
        echo "        <div piwik-field uicontrol=\"text\" name=\"license_key\"
             class=\"valign licenseKeyText\"
             full-width=\"true\"
             ng-model=\"licenseController.licenseKey\"
             ng-change=\"licenseController.updatedLicenseKey()\"
             placeholder=\"";
        // line 7
        if (($context["isValidConsumer"] ?? $this->getContext($context, "isValidConsumer"))) {
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_LicenseKeyIsValidShort")), "html", null, true);
        } else {
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_LicenseKey")), "html_attr");
        }
        echo "\">
        </div>
        <div piwik-save-button
             class=\"valign\"
             onconfirm=\"licenseController.updateLicense()\"
             data-disabled=\"!licenseController.enableUpdate\"
             value=\"";
        // line 13
        if (($context["hasLicenseKey"] ?? $this->getContext($context, "hasLicenseKey"))) {
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CoreUpdater_UpdateTitle")), "html_attr");
        } else {
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_ActivateLicenseKey")), "html_attr");
        }
        echo "\"
             id=\"submit_license_key\"></div>
";
        $context["defaultLicenseKeyFields"] = ('' === $tmp = ob_get_clean()) ? '' : new Twig_Markup($tmp, $this->env->getCharset());
        // line 16
        echo "
<div class=\"marketplace-max-width\" ng-controller=\"PiwikMarketplaceLicenseController as licenseController\">
    <div class=\"marketplace-paid-intro\">
    ";
        // line 19
        if (($context["isValidConsumer"] ?? $this->getContext($context, "isValidConsumer"))) {
            // line 20
            echo "        ";
            if (($context["isSuperUser"] ?? $this->getContext($context, "isSuperUser"))) {
                // line 21
                echo "            ";
                echo call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_PaidPluginsWithLicenseKeyIntro", ""));
                echo "
            <br/>

            <div class=\"licenseToolbar valign-wrapper\">
                ";
                // line 25
                echo ($context["defaultLicenseKeyFields"] ?? $this->getContext($context, "defaultLicenseKeyFields"));
                echo "

                <div piwik-save-button
                     class=\"valign\"
                     id=\"remove_license_key\"
                     onconfirm=\"licenseController.removeLicense()\"
                     value=\"";
                // line 31
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_RemoveLicenseKey")), "html_attr");
                echo "\"
                ></div>

                <a href=\"";
                // line 34
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFunction('linkTo')->getCallable(), array(array("action" => "subscriptionOverview"))), "html", null, true);
                echo "\" class=\"btn valign\">
                    ";
                // line 35
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_ViewSubscriptions")), "html", null, true);
                echo "
                </a>

                ";
                // line 38
                if (((($context["isAutoUpdatePossible"] ?? $this->getContext($context, "isAutoUpdatePossible")) && ($context["isPluginsAdminEnabled"] ?? $this->getContext($context, "isPluginsAdminEnabled"))) && twig_length_filter($this->env, ($context["paidPluginsToInstallAtOnce"] ?? $this->getContext($context, "paidPluginsToInstallAtOnce"))))) {
                    // line 39
                    echo "                    <a href=\"javascript:;\" class=\"btn installAllPaidPlugins valign\">
                        ";
                    // line 40
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_InstallPurchasedPlugins")), "html", null, true);
                    echo "
                    </a>
                    ";
                    // line 42
                    $this->loadTemplate("@Marketplace/paid-plugins-install-list.twig", "@Marketplace/licenseform.twig", 42)->display($context);
                    // line 43
                    echo "                ";
                }
                // line 44
                echo "
            </div>

            <div piwik-activity-indicator loading=\"licenseController.isUpdating\"></div>
        ";
            }
            // line 49
            echo "
    ";
        } else {
            // line 51
            echo "        ";
            if (($context["isSuperUser"] ?? $this->getContext($context, "isSuperUser"))) {
                // line 52
                echo "            ";
                echo call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_PaidPluginsNoLicenseKeyIntro", "<a target='_blank' rel='noreferrer noopener' href='https://matomo.org/recommends/premium-plugins/'>", "</a>"));
                echo "

            <br/>

            <div class=\"licenseToolbar valign-wrapper\">
                ";
                // line 57
                echo ($context["defaultLicenseKeyFields"] ?? $this->getContext($context, "defaultLicenseKeyFields"));
                echo "
            </div>

            <div piwik-activity-indicator loading=\"licenseController.isUpdating\"></div>

        ";
            } else {
                // line 63
                echo "            ";
                echo call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_PaidPluginsNoLicenseKeyIntroNoSuperUserAccess", "<a target='_blank' rel='noreferrer noopener' href='https://matomo.org/recommends/premium-plugins/'>", "</a>"));
                echo "
        ";
            }
            // line 65
            echo "
    ";
        }
        // line 67
        echo "    </div>
</div>


<div class=\"ui-confirm\" id=\"confirmRemoveLicense\">
    <h2>";
        // line 72
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_ConfirmRemoveLicense")), "html", null, true);
        echo "</h2>
    <input role=\"yes\" type=\"button\" value=\"";
        // line 73
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Yes")), "html", null, true);
        echo "\"/>
    <input role=\"no\" type=\"button\" value=\"";
        // line 74
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_No")), "html", null, true);
        echo "\"/>
</div>
";
    }

    public function getTemplateName()
    {
        return "@Marketplace/licenseform.twig";
    }

    public function isTraitable()
    {
        return false;
    }

    public function getDebugInfo()
    {
        return array (  166 => 74,  162 => 73,  158 => 72,  151 => 67,  147 => 65,  141 => 63,  132 => 57,  123 => 52,  120 => 51,  116 => 49,  109 => 44,  106 => 43,  104 => 42,  99 => 40,  96 => 39,  94 => 38,  88 => 35,  84 => 34,  78 => 31,  69 => 25,  61 => 21,  58 => 20,  56 => 19,  51 => 16,  41 => 13,  28 => 7,  21 => 2,  19 => 1,);
    }

    /** @deprecated since 1.27 (to be removed in 2.0). Use getSourceContext() instead */
    public function getSource()
    {
        @trigger_error('The '.__METHOD__.' method is deprecated since version 1.27 and will be removed in 2.0. Use getSourceContext() instead.', E_USER_DEPRECATED);

        return $this->getSourceContext()->getCode();
    }

    public function getSourceContext()
    {
        return new Twig_Source("{% set defaultLicenseKeyFields %}
        <div piwik-field uicontrol=\"text\" name=\"license_key\"
             class=\"valign licenseKeyText\"
             full-width=\"true\"
             ng-model=\"licenseController.licenseKey\"
             ng-change=\"licenseController.updatedLicenseKey()\"
             placeholder=\"{% if isValidConsumer %}{{ 'Marketplace_LicenseKeyIsValidShort'|translate }}{% else %}{{ 'Marketplace_LicenseKey'|translate|e('html_attr') }}{% endif %}\">
        </div>
        <div piwik-save-button
             class=\"valign\"
             onconfirm=\"licenseController.updateLicense()\"
             data-disabled=\"!licenseController.enableUpdate\"
             value=\"{% if hasLicenseKey %}{{ 'CoreUpdater_UpdateTitle'|translate|e('html_attr') }}{% else %}{{ 'Marketplace_ActivateLicenseKey'|translate|e('html_attr') }}{% endif %}\"
             id=\"submit_license_key\"></div>
{% endset %}

<div class=\"marketplace-max-width\" ng-controller=\"PiwikMarketplaceLicenseController as licenseController\">
    <div class=\"marketplace-paid-intro\">
    {% if isValidConsumer %}
        {% if isSuperUser %}
            {{ 'Marketplace_PaidPluginsWithLicenseKeyIntro'|translate('')|raw }}
            <br/>

            <div class=\"licenseToolbar valign-wrapper\">
                {{ defaultLicenseKeyFields|raw }}

                <div piwik-save-button
                     class=\"valign\"
                     id=\"remove_license_key\"
                     onconfirm=\"licenseController.removeLicense()\"
                     value=\"{{ 'Marketplace_RemoveLicenseKey'|translate|e('html_attr') }}\"
                ></div>

                <a href=\"{{ linkTo({'action': 'subscriptionOverview'}) }}\" class=\"btn valign\">
                    {{ 'Marketplace_ViewSubscriptions'|translate }}
                </a>

                {% if isAutoUpdatePossible and isPluginsAdminEnabled and paidPluginsToInstallAtOnce|length %}
                    <a href=\"javascript:;\" class=\"btn installAllPaidPlugins valign\">
                        {{ 'Marketplace_InstallPurchasedPlugins'|translate }}
                    </a>
                    {% include '@Marketplace/paid-plugins-install-list.twig' %}
                {% endif %}

            </div>

            <div piwik-activity-indicator loading=\"licenseController.isUpdating\"></div>
        {% endif %}

    {% else %}
        {% if isSuperUser %}
            {{ 'Marketplace_PaidPluginsNoLicenseKeyIntro'|translate(\"<a target='_blank' rel='noreferrer noopener' href='https://matomo.org/recommends/premium-plugins/'>\", \"</a>\")|raw }}

            <br/>

            <div class=\"licenseToolbar valign-wrapper\">
                {{ defaultLicenseKeyFields|raw }}
            </div>

            <div piwik-activity-indicator loading=\"licenseController.isUpdating\"></div>

        {% else %}
            {{ 'Marketplace_PaidPluginsNoLicenseKeyIntroNoSuperUserAccess'|translate(\"<a target='_blank' rel='noreferrer noopener' href='https://matomo.org/recommends/premium-plugins/'>\", \"</a>\")|raw }}
        {% endif %}

    {% endif %}
    </div>
</div>


<div class=\"ui-confirm\" id=\"confirmRemoveLicense\">
    <h2>{{ 'Marketplace_ConfirmRemoveLicense'|translate }}</h2>
    <input role=\"yes\" type=\"button\" value=\"{{ 'General_Yes'|translate }}\"/>
    <input role=\"no\" type=\"button\" value=\"{{ 'General_No'|translate }}\"/>
</div>
", "@Marketplace/licenseform.twig", "D:\\xampp\\htdocs\\matomo\\plugins\\Marketplace\\templates\\licenseform.twig");
    }
}
