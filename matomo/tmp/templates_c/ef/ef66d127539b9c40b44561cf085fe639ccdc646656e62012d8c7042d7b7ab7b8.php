<?php

/* _jsGlobalVariables.twig */
class __TwigTemplate_e10ebbb19e60a6e0cd37646c92622803c414b6fed3d813e6c0504ee59838b2df extends Twig_Template
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
        echo "<script type=\"text/javascript\">
    var piwik = {};
    piwik.token_auth = \"";
        // line 3
        echo \Piwik\piwik_escape_filter($this->env, ($context["token_auth"] ?? $this->getContext($context, "token_auth")), "html", null, true);
        echo "\";
    piwik.piwik_url = \"";
        // line 4
        echo \Piwik\piwik_escape_filter($this->env, ($context["piwikUrl"] ?? $this->getContext($context, "piwikUrl")), "html", null, true);
        echo "\";
    piwik.cacheBuster = \"";
        // line 5
        echo \Piwik\piwik_escape_filter($this->env, ($context["cacheBuster"] ?? $this->getContext($context, "cacheBuster")), "html", null, true);
        echo "\";
    ";
        // line 6
        if ((isset($context["timezoneOffset"]) || array_key_exists("timezoneOffset", $context))) {
            echo "piwik.timezoneOffset = ";
            echo \Piwik\piwik_escape_filter($this->env, ($context["timezoneOffset"] ?? $this->getContext($context, "timezoneOffset")), "html", null, true);
            echo ";";
        }
        // line 7
        echo "
    piwik.numbers = {
        patternNumber: \"";
        // line 9
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Intl_NumberFormatNumber")), "html", null, true);
        echo "\",
        patternPercent: \"";
        // line 10
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Intl_NumberFormatPercent")), "html", null, true);
        echo "\",
        patternCurrency: \"";
        // line 11
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Intl_NumberFormatCurrency")), "html", null, true);
        echo "\",
        symbolPlus: \"";
        // line 12
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Intl_NumberSymbolPlus")), "html", null, true);
        echo "\",
        symbolMinus: \"";
        // line 13
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Intl_NumberSymbolMinus")), "html", null, true);
        echo "\",
        symbolPercent: \"";
        // line 14
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Intl_NumberSymbolPercent")), "html", null, true);
        echo "\",
        symbolGroup: \"";
        // line 15
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Intl_NumberSymbolGroup")), "html", null, true);
        echo "\",
        symbolDecimal: \"";
        // line 16
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Intl_NumberSymbolDecimal")), "html", null, true);
        echo "\"
    };

    ";
        // line 19
        if (($context["userLogin"] ?? $this->getContext($context, "userLogin"))) {
            echo "piwik.userLogin = \"";
            echo \Piwik\piwik_escape_filter($this->env, \Piwik\piwik_escape_filter($this->env, ($context["userLogin"] ?? $this->getContext($context, "userLogin")), "js"), "html", null, true);
            echo "\";";
        }
        // line 20
        echo "
    ";
        // line 21
        if ((isset($context["idSite"]) || array_key_exists("idSite", $context))) {
            echo "piwik.idSite = \"";
            echo \Piwik\piwik_escape_filter($this->env, ($context["idSite"] ?? $this->getContext($context, "idSite")), "html", null, true);
            echo "\";";
        }
        // line 22
        echo "
    ";
        // line 23
        if ((isset($context["siteName"]) || array_key_exists("siteName", $context))) {
            echo "piwik.siteName = \"";
            echo \Piwik\piwik_escape_filter($this->env, \Piwik\piwik_escape_filter($this->env, ($context["siteName"] ?? $this->getContext($context, "siteName")), "js"), "html", null, true);
            echo "\";";
        }
        // line 24
        echo "
    ";
        // line 25
        if ((isset($context["siteMainUrl"]) || array_key_exists("siteMainUrl", $context))) {
            echo "piwik.siteMainUrl = \"";
            echo \Piwik\piwik_escape_filter($this->env, \Piwik\piwik_escape_filter($this->env, ($context["siteMainUrl"] ?? $this->getContext($context, "siteMainUrl")), "js"), "html", null, true);
            echo "\";";
        }
        // line 26
        echo "
    ";
        // line 27
        if ((isset($context["period"]) || array_key_exists("period", $context))) {
            echo "piwik.period = \"";
            echo \Piwik\piwik_escape_filter($this->env, ($context["period"] ?? $this->getContext($context, "period")), "html", null, true);
            echo "\";";
        }
        // line 28
        echo "
";
        // line 33
        echo "    piwik.currentDateString = \"";
        echo \Piwik\piwik_escape_filter($this->env, (((isset($context["date"]) || array_key_exists("date", $context))) ? (_twig_default_filter(($context["date"] ?? $this->getContext($context, "date")), (((isset($context["endDate"]) || array_key_exists("endDate", $context))) ? (_twig_default_filter(($context["endDate"] ?? $this->getContext($context, "endDate")), "")) : ("")))) : ((((isset($context["endDate"]) || array_key_exists("endDate", $context))) ? (_twig_default_filter(($context["endDate"] ?? $this->getContext($context, "endDate")), "")) : ("")))), "html", null, true);
        echo "\";
";
        // line 34
        if ((isset($context["startDate"]) || array_key_exists("startDate", $context))) {
            // line 35
            echo "    piwik.startDateString = \"";
            echo \Piwik\piwik_escape_filter($this->env, ($context["startDate"] ?? $this->getContext($context, "startDate")), "html", null, true);
            echo "\";
    piwik.endDateString = \"";
            // line 36
            echo \Piwik\piwik_escape_filter($this->env, ($context["endDate"] ?? $this->getContext($context, "endDate")), "html", null, true);
            echo "\";
    piwik.minDateYear = ";
            // line 37
            echo \Piwik\piwik_escape_filter($this->env, ($context["minDateYear"] ?? $this->getContext($context, "minDateYear")), "html", null, true);
            echo ";
    piwik.minDateMonth = parseInt(\"";
            // line 38
            echo \Piwik\piwik_escape_filter($this->env, ($context["minDateMonth"] ?? $this->getContext($context, "minDateMonth")), "html", null, true);
            echo "\", 10);
    piwik.minDateDay = parseInt(\"";
            // line 39
            echo \Piwik\piwik_escape_filter($this->env, ($context["minDateDay"] ?? $this->getContext($context, "minDateDay")), "html", null, true);
            echo "\", 10);
    piwik.maxDateYear = ";
            // line 40
            echo \Piwik\piwik_escape_filter($this->env, ($context["maxDateYear"] ?? $this->getContext($context, "maxDateYear")), "html", null, true);
            echo ";
    piwik.maxDateMonth = parseInt(\"";
            // line 41
            echo \Piwik\piwik_escape_filter($this->env, ($context["maxDateMonth"] ?? $this->getContext($context, "maxDateMonth")), "html", null, true);
            echo "\", 10);
    piwik.maxDateDay = parseInt(\"";
            // line 42
            echo \Piwik\piwik_escape_filter($this->env, ($context["maxDateDay"] ?? $this->getContext($context, "maxDateDay")), "html", null, true);
            echo "\", 10);
";
        }
        // line 44
        echo "    ";
        if ((isset($context["language"]) || array_key_exists("language", $context))) {
            echo "piwik.language = \"";
            echo \Piwik\piwik_escape_filter($this->env, ($context["language"] ?? $this->getContext($context, "language")), "html", null, true);
            echo "\";";
        }
        // line 45
        echo "
    piwik.hasSuperUserAccess = ";
        // line 46
        echo \Piwik\piwik_escape_filter($this->env, \Piwik\piwik_escape_filter($this->env, (((isset($context["hasSuperUserAccess"]) || array_key_exists("hasSuperUserAccess", $context))) ? (_twig_default_filter(($context["hasSuperUserAccess"] ?? $this->getContext($context, "hasSuperUserAccess")), 0)) : (0)), "js"), "html", null, true);
        echo ";
    piwik.userCapabilities = ";
        // line 47
        echo twig_jsonencode_filter((((isset($context["userCapabilities"]) || array_key_exists("userCapabilities", $context))) ? (_twig_default_filter(($context["userCapabilities"] ?? $this->getContext($context, "userCapabilities")), array())) : (array())));
        echo ";
    piwik.config = {};
";
        // line 49
        if ((isset($context["clientSideConfig"]) || array_key_exists("clientSideConfig", $context))) {
            // line 50
            echo "    piwik.config = ";
            echo twig_jsonencode_filter(($context["clientSideConfig"] ?? $this->getContext($context, "clientSideConfig")));
            echo ";
";
        }
        // line 52
        echo "    piwik.shouldPropagateTokenAuth = ";
        echo twig_jsonencode_filter(($context["shouldPropagateTokenAuth"] ?? $this->getContext($context, "shouldPropagateTokenAuth")));
        echo ";
    ";
        // line 53
        echo call_user_func_array($this->env->getFunction('postEvent')->getCallable(), array("Template.jsGlobalVariables"));
        echo "
</script>
";
    }

    public function getTemplateName()
    {
        return "_jsGlobalVariables.twig";
    }

    public function isTraitable()
    {
        return false;
    }

    public function getDebugInfo()
    {
        return array (  197 => 53,  192 => 52,  186 => 50,  184 => 49,  179 => 47,  175 => 46,  172 => 45,  165 => 44,  160 => 42,  156 => 41,  152 => 40,  148 => 39,  144 => 38,  140 => 37,  136 => 36,  131 => 35,  129 => 34,  124 => 33,  121 => 28,  115 => 27,  112 => 26,  106 => 25,  103 => 24,  97 => 23,  94 => 22,  88 => 21,  85 => 20,  79 => 19,  73 => 16,  69 => 15,  65 => 14,  61 => 13,  57 => 12,  53 => 11,  49 => 10,  45 => 9,  41 => 7,  35 => 6,  31 => 5,  27 => 4,  23 => 3,  19 => 1,);
    }

    /** @deprecated since 1.27 (to be removed in 2.0). Use getSourceContext() instead */
    public function getSource()
    {
        @trigger_error('The '.__METHOD__.' method is deprecated since version 1.27 and will be removed in 2.0. Use getSourceContext() instead.', E_USER_DEPRECATED);

        return $this->getSourceContext()->getCode();
    }

    public function getSourceContext()
    {
        return new Twig_Source("<script type=\"text/javascript\">
    var piwik = {};
    piwik.token_auth = \"{{ token_auth }}\";
    piwik.piwik_url = \"{{ piwikUrl }}\";
    piwik.cacheBuster = \"{{ cacheBuster }}\";
    {% if timezoneOffset is defined %}piwik.timezoneOffset = {{ timezoneOffset }};{% endif %}

    piwik.numbers = {
        patternNumber: \"{{ 'Intl_NumberFormatNumber'|translate }}\",
        patternPercent: \"{{ 'Intl_NumberFormatPercent'|translate }}\",
        patternCurrency: \"{{ 'Intl_NumberFormatCurrency'|translate }}\",
        symbolPlus: \"{{ 'Intl_NumberSymbolPlus'|translate }}\",
        symbolMinus: \"{{ 'Intl_NumberSymbolMinus'|translate }}\",
        symbolPercent: \"{{ 'Intl_NumberSymbolPercent'|translate }}\",
        symbolGroup: \"{{ 'Intl_NumberSymbolGroup'|translate }}\",
        symbolDecimal: \"{{ 'Intl_NumberSymbolDecimal'|translate }}\"
    };

    {% if userLogin %}piwik.userLogin = \"{{ userLogin|e('js')}}\";{% endif %}

    {% if idSite is defined %}piwik.idSite = \"{{ idSite }}\";{% endif %}

    {% if siteName is defined %}piwik.siteName = \"{{ siteName|e('js') }}\";{% endif %}

    {% if siteMainUrl is defined %}piwik.siteMainUrl = \"{{ siteMainUrl|e('js') }}\";{% endif %}

    {% if period is defined %}piwik.period = \"{{ period }}\";{% endif %}

{# piwik.currentDateString should not be used other than by the calendar Javascript
            (it is not set to the expected value when period=range)
        Use broadcast.getValueFromUrl('date') instead
#}
    piwik.currentDateString = \"{{ date|default(endDate|default('')) }}\";
{% if startDate is defined %}
    piwik.startDateString = \"{{ startDate }}\";
    piwik.endDateString = \"{{ endDate }}\";
    piwik.minDateYear = {{ minDateYear }};
    piwik.minDateMonth = parseInt(\"{{ minDateMonth }}\", 10);
    piwik.minDateDay = parseInt(\"{{ minDateDay }}\", 10);
    piwik.maxDateYear = {{ maxDateYear }};
    piwik.maxDateMonth = parseInt(\"{{ maxDateMonth }}\", 10);
    piwik.maxDateDay = parseInt(\"{{ maxDateDay }}\", 10);
{% endif %}
    {% if language is defined %}piwik.language = \"{{ language }}\";{% endif %}

    piwik.hasSuperUserAccess = {{ hasSuperUserAccess|default(0)|e('js')}};
    piwik.userCapabilities = {{ userCapabilities|default([])|json_encode|raw }};
    piwik.config = {};
{% if clientSideConfig is defined %}
    piwik.config = {{ clientSideConfig|json_encode|raw }};
{% endif %}
    piwik.shouldPropagateTokenAuth = {{ shouldPropagateTokenAuth|json_encode|raw }};
    {{ postEvent(\"Template.jsGlobalVariables\") }}
</script>
", "_jsGlobalVariables.twig", "D:\\xampp\\htdocs\\matomo\\plugins\\Morpheus\\templates\\_jsGlobalVariables.twig");
    }
}
