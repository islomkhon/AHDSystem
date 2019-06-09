<?php

/* admin.twig */
class __TwigTemplate_47ca1da0040071617932b94d14d1a74b3fd05d28fa30d31ae8719a696b169b16 extends Twig_Template
{
    public function __construct(Twig_Environment $env)
    {
        parent::__construct($env);

        // line 1
        $this->parent = $this->loadTemplate("layout.twig", "admin.twig", 1);
        $this->blocks = array(
            'body' => array($this, 'block_body'),
            'root' => array($this, 'block_root'),
            'topcontrols' => array($this, 'block_topcontrols'),
            'notification' => array($this, 'block_notification'),
            'content' => array($this, 'block_content'),
        );
    }

    protected function doGetParent(array $context)
    {
        return "layout.twig";
    }

    protected function doDisplay(array $context, array $blocks = array())
    {
        // line 3
        ob_start();
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CoreAdminHome_Administration")), "html", null, true);
        $context["categoryTitle"] = ('' === $tmp = ob_get_clean()) ? '' : new Twig_Markup($tmp, $this->env->getCharset());
        // line 5
        $context["bodyClass"] = call_user_func_array($this->env->getFunction('postEvent')->getCallable(), array("Template.bodyClass", "admin"));
        // line 6
        $context["isAdminArea"] = true;
        // line 1
        $this->parent->display($context, array_merge($this->blocks, $blocks));
    }

    // line 8
    public function block_body($context, array $blocks = array())
    {
        // line 9
        echo "    ";
        $context["topMenuModule"] = "CoreAdminHome";
        // line 10
        echo "    ";
        $context["topMenuAction"] = "home";
        // line 11
        echo "    ";
        echo call_user_func_array($this->env->getFunction('postEvent')->getCallable(), array("Template.header", "admin"));
        echo "
    ";
        // line 12
        $this->displayParentBlock("body", $context, $blocks);
        echo "
    ";
        // line 13
        echo call_user_func_array($this->env->getFunction('postEvent')->getCallable(), array("Template.footer", "admin"));
        echo "
";
    }

    // line 17
    public function block_root($context, array $blocks = array())
    {
        // line 18
        echo "    ";
        $this->loadTemplate("@CoreHome/_topScreen.twig", "admin.twig", 18)->display($context);
        // line 19
        echo "
    <div class=\"top_controls\">
        <div piwik-quick-access ng-cloak class=\"piwikTopControl borderedControl\"></div>

        ";
        // line 23
        $this->displayBlock('topcontrols', $context, $blocks);
        // line 25
        echo "
        ";
        // line 26
        $this->loadTemplate("@CoreHome/_headerMessage.twig", "admin.twig", 26)->display($context);
        // line 27
        echo "    </div>

    ";
        // line 29
        $context["ajax"] = $this->loadTemplate("ajaxMacros.twig", "admin.twig", 29);
        // line 30
        echo "    ";
        echo $context["ajax"]->getrequestErrorDiv((((isset($context["emailSuperUser"]) || array_key_exists("emailSuperUser", $context))) ? (_twig_default_filter(($context["emailSuperUser"] ?? $this->getContext($context, "emailSuperUser")), "")) : ("")), ($context["areAdsForProfessionalServicesEnabled"] ?? $this->getContext($context, "areAdsForProfessionalServicesEnabled")), ($context["currentModule"] ?? $this->getContext($context, "currentModule")));
        echo "
    ";
        // line 31
        echo call_user_func_array($this->env->getFunction('postEvent')->getCallable(), array("Template.beforeContent", "admin", ($context["currentModule"] ?? $this->getContext($context, "currentModule")), ($context["currentAction"] ?? $this->getContext($context, "currentAction"))));
        echo "

    <div class=\"page\">

        ";
        // line 35
        if (( !(isset($context["showMenu"]) || array_key_exists("showMenu", $context)) || ($context["showMenu"] ?? $this->getContext($context, "showMenu")))) {
            // line 36
            echo "            ";
            $context["menu"] = $this->loadTemplate("@CoreHome/_menu.twig", "admin.twig", 36);
            // line 37
            echo "            ";
            echo $context["menu"]->getmenu(($context["adminMenu"] ?? $this->getContext($context, "adminMenu")), false, "Menu--admin", ($context["currentModule"] ?? $this->getContext($context, "currentModule")), ($context["currentAction"] ?? $this->getContext($context, "currentAction")));
            echo "
        ";
        }
        // line 39
        echo "

        <div class=\"pageWrap\">
            <a name=\"main\"></a>
            ";
        // line 43
        $this->displayBlock('notification', $context, $blocks);
        // line 46
        echo "            ";
        $this->loadTemplate("@CoreHome/_warningInvalidHost.twig", "admin.twig", 46)->display($context);
        // line 47
        echo "
            <div class=\"admin\" id=\"content\" ng-cloak>

                <div class=\"ui-confirm\" id=\"alert\">
                    <h2></h2>
                    <input role=\"no\" type=\"button\" value=\"";
        // line 52
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Ok")), "html", null, true);
        echo "\"/>
                </div>

                ";
        // line 55
        $this->displayBlock('content', $context, $blocks);
        // line 57
        echo "

            </div>
        </div>
    </div>


";
    }

    // line 23
    public function block_topcontrols($context, array $blocks = array())
    {
        // line 24
        echo "        ";
    }

    // line 43
    public function block_notification($context, array $blocks = array())
    {
        // line 44
        echo "                ";
        $this->loadTemplate("@CoreHome/_notifications.twig", "admin.twig", 44)->display($context);
        // line 45
        echo "            ";
    }

    // line 55
    public function block_content($context, array $blocks = array())
    {
        // line 56
        echo "                ";
    }

    public function getTemplateName()
    {
        return "admin.twig";
    }

    public function isTraitable()
    {
        return false;
    }

    public function getDebugInfo()
    {
        return array (  170 => 56,  167 => 55,  163 => 45,  160 => 44,  157 => 43,  153 => 24,  150 => 23,  139 => 57,  137 => 55,  131 => 52,  124 => 47,  121 => 46,  119 => 43,  113 => 39,  107 => 37,  104 => 36,  102 => 35,  95 => 31,  90 => 30,  88 => 29,  84 => 27,  82 => 26,  79 => 25,  77 => 23,  71 => 19,  68 => 18,  65 => 17,  59 => 13,  55 => 12,  50 => 11,  47 => 10,  44 => 9,  41 => 8,  37 => 1,  35 => 6,  33 => 5,  29 => 3,  11 => 1,);
    }

    /** @deprecated since 1.27 (to be removed in 2.0). Use getSourceContext() instead */
    public function getSource()
    {
        @trigger_error('The '.__METHOD__.' method is deprecated since version 1.27 and will be removed in 2.0. Use getSourceContext() instead.', E_USER_DEPRECATED);

        return $this->getSourceContext()->getCode();
    }

    public function getSourceContext()
    {
        return new Twig_Source("{% extends 'layout.twig' %}

{% set categoryTitle %}{{ 'CoreAdminHome_Administration'|translate }}{% endset %}

{% set bodyClass = postEvent('Template.bodyClass', 'admin') %}
{% set isAdminArea = true %}

{% block body %}
    {% set topMenuModule = 'CoreAdminHome' %}
    {% set topMenuAction = 'home' %}
    {{ postEvent(\"Template.header\", \"admin\") }}
    {{ parent() }}
    {{ postEvent(\"Template.footer\", \"admin\") }}
{% endblock %}


{% block root %}
    {% include \"@CoreHome/_topScreen.twig\" %}

    <div class=\"top_controls\">
        <div piwik-quick-access ng-cloak class=\"piwikTopControl borderedControl\"></div>

        {% block topcontrols %}
        {% endblock %}

        {% include \"@CoreHome/_headerMessage.twig\" %}
    </div>

    {% import 'ajaxMacros.twig' as ajax %}
    {{ ajax.requestErrorDiv(emailSuperUser|default(''), areAdsForProfessionalServicesEnabled, currentModule) }}
    {{ postEvent(\"Template.beforeContent\", \"admin\", currentModule, currentAction) }}

    <div class=\"page\">

        {% if showMenu is not defined or showMenu %}
            {% import '@CoreHome/_menu.twig' as menu %}
            {{ menu.menu(adminMenu, false, 'Menu--admin', currentModule, currentAction) }}
        {% endif %}


        <div class=\"pageWrap\">
            <a name=\"main\"></a>
            {% block notification %}
                {% include \"@CoreHome/_notifications.twig\" %}
            {% endblock %}
            {% include \"@CoreHome/_warningInvalidHost.twig\" %}

            <div class=\"admin\" id=\"content\" ng-cloak>

                <div class=\"ui-confirm\" id=\"alert\">
                    <h2></h2>
                    <input role=\"no\" type=\"button\" value=\"{{ 'General_Ok'|translate }}\"/>
                </div>

                {% block content %}
                {% endblock %}


            </div>
        </div>
    </div>


{% endblock %}
", "admin.twig", "D:\\xampp\\htdocs\\matomo\\plugins\\Morpheus\\templates\\admin.twig");
    }
}
