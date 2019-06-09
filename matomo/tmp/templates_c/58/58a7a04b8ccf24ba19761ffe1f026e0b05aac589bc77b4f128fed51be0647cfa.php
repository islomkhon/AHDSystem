<?php

/* @IP2Location/config.twig */
class __TwigTemplate_cb692220d183347191af9e9b11e6fa73bc718527e48af572f57deb1ca852375a extends Twig_Template
{
    public function __construct(Twig_Environment $env)
    {
        parent::__construct($env);

        // line 1
        $this->parent = $this->loadTemplate("admin.twig", "@IP2Location/config.twig", 1);
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
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("IP2Location_IP2Location")), "html", null, true);
        $context["title"] = ('' === $tmp = ob_get_clean()) ? '' : new Twig_Markup($tmp, $this->env->getCharset());
        // line 1
        $this->parent->display($context, array_merge($this->blocks, $blocks));
    }

    // line 5
    public function block_content($context, array $blocks = array())
    {
        // line 6
        $context["piwik"] = $this->loadTemplate("macros.twig", "@IP2Location/config.twig", 6);
        // line 7
        if (($context["isSuperUser"] ?? $this->getContext($context, "isSuperUser"))) {
            // line 8
            echo "
\t<h1>";
            // line 9
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("IP2Location_IP2LocationSettings")), "html", null, true);
            echo "</h1>

\t";
            // line 11
            if (($context["saved"] ?? $this->getContext($context, "saved"))) {
                // line 12
                echo "\t<div class=\"card-panel\">
\t\t<span class=\"green-text text-darken-2\">";
                // line 13
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("IP2Location_ChangesHasBeenSaved")), "html", null, true);
                echo "</span>
\t</div>
\t";
            }
            // line 16
            echo "
\t";
            // line 17
            $context['_parent'] = $context;
            $context['_seq'] = twig_ensure_traversable(($context["errors"] ?? $this->getContext($context, "errors")));
            foreach ($context['_seq'] as $context["i"] => $context["error"]) {
                // line 18
                echo "\t<div class=\"card-panel\">
\t\t<span class=\"red-text text-darken-2\">";
                // line 19
                echo \Piwik\piwik_escape_filter($this->env, $context["error"], "html", null, true);
                echo "</span>
\t</div>
\t";
            }
            $_parent = $context['_parent'];
            unset($context['_seq'], $context['_iterated'], $context['i'], $context['error'], $context['_parent'], $context['loop']);
            $context = array_intersect_key($context, $_parent) + $_parent;
            // line 22
            echo "
\t<form method=\"POST\" action=\"";
            // line 23
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFunction('linkTo')->getCallable(), array(array("module" => "IP2Location", "action" => "saveConfig"))), "html", null, true);
            echo "\" class=\"col s6\">

\t\t<h3>";
            // line 25
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("IP2Location_LookupMode")), "html", null, true);
            echo "</h3>

\t\t<div class=\"form-group row\">
\t\t\t<div class=\"col s12\">
\t\t\t\t<p>
\t\t\t\t\t<input type=\"radio\" value=\"BIN\" id=\"lookupMode_BIN\" name=\"lookupMode\" ";
            // line 30
            if ((($context["lookupMode"] ?? $this->getContext($context, "lookupMode")) == "BIN")) {
                echo " checked=\"checked\"";
            }
            echo " />
\t\t\t\t\t<label for=\"lookupMode_BIN\">
\t\t\t\t\t    ";
            // line 32
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("IP2Location_LocalBINDatabase")), "html", null, true);
            echo "
\t\t\t\t\t    <span class=\"form-description\">";
            // line 33
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("IP2Location_UploadYourIP2LocationBINDatabaseToPiwikMisc")), "html", null, true);
            echo "</span>
\t\t\t\t\t</label>
\t\t\t\t</p>
\t\t\t\t<p>
\t\t\t\t\t<input type=\"radio\" value=\"WS\" id=\"lookupMode_WS\" name=\"lookupMode\" ";
            // line 37
            if ((($context["lookupMode"] ?? $this->getContext($context, "lookupMode")) == "WS")) {
                echo " checked=\"checked\"";
            }
            echo " />
\t\t\t\t\t<label for=\"lookupMode_WS\">
\t\t\t\t\t    ";
            // line 39
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("IP2Location_WebService")), "html", null, true);
            echo "
\t\t\t\t\t</label>
\t\t\t\t</p>
\t\t\t</div>
\t\t</div>

\t\t<div class=\"form-group row\">
\t\t\t<div class=\"input-field col s12\">
\t\t\t\t<input type=\"text\" value=\"";
            // line 47
            echo \Piwik\piwik_escape_filter($this->env, ($context["apiKey"] ?? $this->getContext($context, "apiKey")), "html", null, true);
            echo "\" id=\"apiKey\" name=\"apiKey\" />
\t\t\t\t<label for=\"apiKey\">";
            // line 48
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("IP2Location_APIKey")), "html", null, true);
            echo "</label>
\t\t\t\t<span class=\"form-description\">";
            // line 49
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("IP2Location_PleaseGetYourAPIKeyFrom")), "html", null, true);
            echo "</span>
\t\t\t</div>
\t\t</div>

\t\t<div class=\"form-group row\">
\t\t\t<button class=\"waves-effect waves-light btn\">";
            // line 54
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("IP2Location_SaveChanges")), "html", null, true);
            echo "</button>
\t\t</div>

\t\t";
            // line 57
            if ((($context["lookupMode"] ?? $this->getContext($context, "lookupMode")) == "BIN")) {
                // line 58
                echo "
\t\t<h3>";
                // line 59
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("IP2Location_DatabaseInformation")), "html", null, true);
                echo "</h3>

\t\t<div class=\"form-group row\">
\t\t\t<div class=\"input-field col s12\">
\t\t\t\t<input type=\"text\" value=\"";
                // line 63
                echo \Piwik\piwik_escape_filter($this->env, ($context["database"] ?? $this->getContext($context, "database")), "html", null, true);
                echo "\" id=\"databaseFile\" disabled>
\t\t\t\t<label for=\"databaseFile\">";
                // line 64
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("IP2Location_IP2LocationDatabaseFile")), "html", null, true);
                echo "</label>
\t\t\t</div>
\t\t</div>
\t\t<div class=\"form-group row\">
\t\t\t<div class=\"input-field col s12\">
\t\t\t\t<input type=\"text\" value=\"";
                // line 69
                echo \Piwik\piwik_escape_filter($this->env, ($context["date"] ?? $this->getContext($context, "date")), "html", null, true);
                echo "\" id=\"databaseDate\" disabled>
\t\t\t\t<label for=\"databaseDate\">";
                // line 70
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("IP2Location_DatabaseDate")), "html", null, true);
                echo "</label>
\t\t\t</div>
\t\t</div>
\t\t<div class=\"form-group row\">
\t\t\t<div class=\"input-field col s12\">
\t\t\t\t<input type=\"text\" value=\"";
                // line 75
                echo \Piwik\piwik_escape_filter($this->env, ($context["size"] ?? $this->getContext($context, "size")), "html", null, true);
                echo "\" id=\"databaseSize\" disabled>
\t\t\t\t<label for=\"databaseSize\">";
                // line 76
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("IP2Location_DatabaseSize")), "html", null, true);
                echo "</label>

\t\t\t\t<small class=\"form-text text-muted\">
\t\t\t\t\t";
                // line 79
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("IP2Location_AutomatedUpdate")), "html", null, true);
                echo "
\t\t\t\t\t<a href=\"";
                // line 80
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("IP2Location_AutomatedUpdateURL")), "html", null, true);
                echo "\" target=\"_blank\">";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("IP2Location_LearnMore")), "html", null, true);
                echo "</a>
\t\t\t\t</small>
\t\t\t</div>
\t\t</div>

\t\t";
            }
            // line 86
            echo "
\t\t";
            // line 87
            if ((($context["lookupMode"] ?? $this->getContext($context, "lookupMode")) == "WS")) {
                // line 88
                echo "
\t\t<h3>";
                // line 89
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("IP2Location_WebServiceInformation")), "html", null, true);
                echo "</h3>

\t\t<div class=\"form-group row\">
\t\t\t<div class=\"input-field col s12\">
\t\t\t\t<input type=\"text\" value=\"";
                // line 93
                echo \Piwik\piwik_escape_filter($this->env, ($context["credit"] ?? $this->getContext($context, "credit")), "html", null, true);
                echo "\" id=\"creditAvailable\" disabled>
\t\t\t\t<label for=\"creditAvailable\">";
                // line 94
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("IP2Location_CreditAvailable")), "html", null, true);
                echo "</label>
\t\t\t</div>
\t\t</div>
\t\t";
            }
            // line 98
            echo "
\t\t<input type=\"hidden\" name=\"submit\" value=\"true\" />
\t</form>
";
        }
        // line 102
        echo "
";
    }

    public function getTemplateName()
    {
        return "@IP2Location/config.twig";
    }

    public function isTraitable()
    {
        return false;
    }

    public function getDebugInfo()
    {
        return array (  245 => 102,  239 => 98,  232 => 94,  228 => 93,  221 => 89,  218 => 88,  216 => 87,  213 => 86,  202 => 80,  198 => 79,  192 => 76,  188 => 75,  180 => 70,  176 => 69,  168 => 64,  164 => 63,  157 => 59,  154 => 58,  152 => 57,  146 => 54,  138 => 49,  134 => 48,  130 => 47,  119 => 39,  112 => 37,  105 => 33,  101 => 32,  94 => 30,  86 => 25,  81 => 23,  78 => 22,  69 => 19,  66 => 18,  62 => 17,  59 => 16,  53 => 13,  50 => 12,  48 => 11,  43 => 9,  40 => 8,  38 => 7,  36 => 6,  33 => 5,  29 => 1,  25 => 3,  11 => 1,);
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

{% set title %}{{'IP2Location_IP2Location'|translate}}{% endset %}

{% block content %}
{% import 'macros.twig' as piwik %}
{% if isSuperUser %}

\t<h1>{{'IP2Location_IP2LocationSettings'|translate}}</h1>

\t{% if saved %}
\t<div class=\"card-panel\">
\t\t<span class=\"green-text text-darken-2\">{{'IP2Location_ChangesHasBeenSaved'|translate}}</span>
\t</div>
\t{% endif %}

\t{% for i, error in errors %}
\t<div class=\"card-panel\">
\t\t<span class=\"red-text text-darken-2\">{{ error }}</span>
\t</div>
\t{% endfor %}

\t<form method=\"POST\" action=\"{{ linkTo({'module':'IP2Location','action':'saveConfig'}) }}\" class=\"col s6\">

\t\t<h3>{{'IP2Location_LookupMode'|translate}}</h3>

\t\t<div class=\"form-group row\">
\t\t\t<div class=\"col s12\">
\t\t\t\t<p>
\t\t\t\t\t<input type=\"radio\" value=\"BIN\" id=\"lookupMode_BIN\" name=\"lookupMode\" {% if lookupMode == 'BIN' %} checked=\"checked\"{% endif %} />
\t\t\t\t\t<label for=\"lookupMode_BIN\">
\t\t\t\t\t    {{ 'IP2Location_LocalBINDatabase'|translate }}
\t\t\t\t\t    <span class=\"form-description\">{{ 'IP2Location_UploadYourIP2LocationBINDatabaseToPiwikMisc'|translate }}</span>
\t\t\t\t\t</label>
\t\t\t\t</p>
\t\t\t\t<p>
\t\t\t\t\t<input type=\"radio\" value=\"WS\" id=\"lookupMode_WS\" name=\"lookupMode\" {% if lookupMode == 'WS' %} checked=\"checked\"{% endif %} />
\t\t\t\t\t<label for=\"lookupMode_WS\">
\t\t\t\t\t    {{ 'IP2Location_WebService'|translate }}
\t\t\t\t\t</label>
\t\t\t\t</p>
\t\t\t</div>
\t\t</div>

\t\t<div class=\"form-group row\">
\t\t\t<div class=\"input-field col s12\">
\t\t\t\t<input type=\"text\" value=\"{{apiKey}}\" id=\"apiKey\" name=\"apiKey\" />
\t\t\t\t<label for=\"apiKey\">{{'IP2Location_APIKey'|translate}}</label>
\t\t\t\t<span class=\"form-description\">{{ 'IP2Location_PleaseGetYourAPIKeyFrom'|translate }}</span>
\t\t\t</div>
\t\t</div>

\t\t<div class=\"form-group row\">
\t\t\t<button class=\"waves-effect waves-light btn\">{{'IP2Location_SaveChanges'|translate}}</button>
\t\t</div>

\t\t{% if lookupMode == 'BIN' %}

\t\t<h3>{{'IP2Location_DatabaseInformation'|translate}}</h3>

\t\t<div class=\"form-group row\">
\t\t\t<div class=\"input-field col s12\">
\t\t\t\t<input type=\"text\" value=\"{{database}}\" id=\"databaseFile\" disabled>
\t\t\t\t<label for=\"databaseFile\">{{'IP2Location_IP2LocationDatabaseFile'|translate}}</label>
\t\t\t</div>
\t\t</div>
\t\t<div class=\"form-group row\">
\t\t\t<div class=\"input-field col s12\">
\t\t\t\t<input type=\"text\" value=\"{{date}}\" id=\"databaseDate\" disabled>
\t\t\t\t<label for=\"databaseDate\">{{'IP2Location_DatabaseDate'|translate}}</label>
\t\t\t</div>
\t\t</div>
\t\t<div class=\"form-group row\">
\t\t\t<div class=\"input-field col s12\">
\t\t\t\t<input type=\"text\" value=\"{{size}}\" id=\"databaseSize\" disabled>
\t\t\t\t<label for=\"databaseSize\">{{'IP2Location_DatabaseSize'|translate}}</label>

\t\t\t\t<small class=\"form-text text-muted\">
\t\t\t\t\t{{'IP2Location_AutomatedUpdate'|translate}}
\t\t\t\t\t<a href=\"{{'IP2Location_AutomatedUpdateURL'|translate}}\" target=\"_blank\">{{'IP2Location_LearnMore'|translate}}</a>
\t\t\t\t</small>
\t\t\t</div>
\t\t</div>

\t\t{% endif %}

\t\t{% if lookupMode == 'WS' %}

\t\t<h3>{{'IP2Location_WebServiceInformation'|translate}}</h3>

\t\t<div class=\"form-group row\">
\t\t\t<div class=\"input-field col s12\">
\t\t\t\t<input type=\"text\" value=\"{{credit}}\" id=\"creditAvailable\" disabled>
\t\t\t\t<label for=\"creditAvailable\">{{'IP2Location_CreditAvailable'|translate}}</label>
\t\t\t</div>
\t\t</div>
\t\t{% endif %}

\t\t<input type=\"hidden\" name=\"submit\" value=\"true\" />
\t</form>
{% endif %}

{% endblock %}", "@IP2Location/config.twig", "D:\\xampp\\htdocs\\matomo\\plugins\\IP2Location\\templates\\config.twig");
    }
}
