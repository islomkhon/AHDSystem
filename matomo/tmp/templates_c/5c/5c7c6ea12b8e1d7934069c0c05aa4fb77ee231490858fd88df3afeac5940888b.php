<?php

/* layout.twig */
class __TwigTemplate_524ada0bb82e2f0b345710d40aaa92c4f71480bd85abe2b35101f964216d38e9 extends Twig_Template
{
    public function __construct(Twig_Environment $env)
    {
        parent::__construct($env);

        $this->parent = false;

        $this->blocks = array(
            'head' => array($this, 'block_head'),
            'pageTitle' => array($this, 'block_pageTitle'),
            'pageDescription' => array($this, 'block_pageDescription'),
            'meta' => array($this, 'block_meta'),
            'body' => array($this, 'block_body'),
            'root' => array($this, 'block_root'),
        );
    }

    protected function doDisplay(array $context, array $blocks = array())
    {
        // line 1
        echo "<!DOCTYPE html>
<html id=\"ng-app\" ";
        // line 2
        if ((isset($context["language"]) || array_key_exists("language", $context))) {
            echo "lang=\"";
            echo \Piwik\piwik_escape_filter($this->env, ($context["language"] ?? $this->getContext($context, "language")), "html", null, true);
            echo "\"";
        }
        echo " ng-app=\"piwikApp\">
    <head>
        ";
        // line 4
        $this->displayBlock('head', $context, $blocks);
        // line 32
        echo "    </head>
    <body id=\"";
        // line 33
        echo \Piwik\piwik_escape_filter($this->env, (((isset($context["bodyId"]) || array_key_exists("bodyId", $context))) ? (_twig_default_filter(($context["bodyId"] ?? $this->getContext($context, "bodyId")), "")) : ("")), "html", null, true);
        echo "\" ng-app=\"app\" class=\"";
        echo \Piwik\piwik_escape_filter($this->env, (((isset($context["bodyClass"]) || array_key_exists("bodyClass", $context))) ? (_twig_default_filter(($context["bodyClass"] ?? $this->getContext($context, "bodyClass")), "")) : ("")), "html", null, true);
        echo "\">

    ";
        // line 35
        $this->displayBlock('body', $context, $blocks);
        // line 50
        echo "
        <div id=\"pageFooter\">
            ";
        // line 52
        echo call_user_func_array($this->env->getFunction('postEvent')->getCallable(), array("Template.pageFooter"));
        echo "
        </div>

        ";
        // line 55
        $this->loadTemplate("@CoreHome/_adblockDetect.twig", "layout.twig", 55)->display($context);
        // line 56
        echo "    </body>
</html>
";
    }

    // line 4
    public function block_head($context, array $blocks = array())
    {
        // line 5
        echo "            <meta charset=\"utf-8\">
            <title>";
        // line 7
        $this->displayBlock('pageTitle', $context, $blocks);
        // line 12
        echo "</title>
            <meta http-equiv=\"X-UA-Compatible\" content=\"IE=EDGE,chrome=1\"/>
            <meta name=\"viewport\" content=\"initial-scale=1.0\"/>
            <meta name=\"generator\" content=\"Matomo - free/libre analytics platform\"/>
            <meta name=\"description\" content=\"";
        // line 16
        $this->displayBlock('pageDescription', $context, $blocks);
        echo "\"/>
            <meta name=\"apple-itunes-app\" content=\"app-id=737216887\" />
            <meta name=\"google\" content=\"notranslate\">
            ";
        // line 19
        $this->displayBlock('meta', $context, $blocks);
        // line 22
        echo "
            ";
        // line 23
        $this->loadTemplate("@CoreHome/_favicon.twig", "layout.twig", 23)->display($context);
        // line 24
        echo "            ";
        $this->loadTemplate("@CoreHome/_applePinnedTabIcon.twig", "layout.twig", 24)->display($context);
        // line 25
        echo "            <meta name=\"theme-color\" content=\"#3450A3\">
            ";
        // line 26
        $this->loadTemplate("_jsGlobalVariables.twig", "layout.twig", 26)->display($context);
        // line 27
        echo "            ";
        $this->loadTemplate("_jsCssIncludes.twig", "layout.twig", 27)->display($context);
        // line 29
        if ( !($context["isCustomLogo"] ?? $this->getContext($context, "isCustomLogo"))) {
            echo "<link rel=\"manifest\" href=\"plugins/CoreHome/javascripts/manifest.json\">";
        }
        // line 30
        echo "
        ";
    }

    // line 7
    public function block_pageTitle($context, array $blocks = array())
    {
        // line 8
        if ((isset($context["title"]) || array_key_exists("title", $context))) {
            echo \Piwik\piwik_escape_filter($this->env, ($context["title"] ?? $this->getContext($context, "title")), "html", null, true);
            echo " - ";
        }
        // line 9
        if ((isset($context["categoryTitle"]) || array_key_exists("categoryTitle", $context))) {
            echo \Piwik\piwik_escape_filter($this->env, ($context["categoryTitle"] ?? $this->getContext($context, "categoryTitle")), "html", null, true);
            echo " - ";
        }
        // line 10
        echo "Matomo";
    }

    // line 16
    public function block_pageDescription($context, array $blocks = array())
    {
    }

    // line 19
    public function block_meta($context, array $blocks = array())
    {
        // line 20
        echo "                <meta name=\"robots\" content=\"noindex,nofollow\">
            ";
    }

    // line 35
    public function block_body($context, array $blocks = array())
    {
        // line 36
        echo "
        ";
        // line 37
        $this->loadTemplate("_iframeBuster.twig", "layout.twig", 37)->display($context);
        // line 38
        echo "        ";
        $this->loadTemplate("@CoreHome/_javaScriptDisabled.twig", "layout.twig", 38)->display($context);
        // line 39
        echo "
        <div id=\"root\">
            ";
        // line 41
        $this->displayBlock('root', $context, $blocks);
        // line 43
        echo "        </div>

        <div piwik-popover-handler></div>

        ";
        // line 47
        $this->loadTemplate("@CoreHome/_shortcuts.twig", "layout.twig", 47)->display($context);
        // line 48
        echo "
    ";
    }

    // line 41
    public function block_root($context, array $blocks = array())
    {
        // line 42
        echo "            ";
    }

    public function getTemplateName()
    {
        return "layout.twig";
    }

    public function isTraitable()
    {
        return false;
    }

    public function getDebugInfo()
    {
        return array (  179 => 42,  176 => 41,  171 => 48,  169 => 47,  163 => 43,  161 => 41,  157 => 39,  154 => 38,  152 => 37,  149 => 36,  146 => 35,  141 => 20,  138 => 19,  133 => 16,  129 => 10,  124 => 9,  119 => 8,  116 => 7,  111 => 30,  107 => 29,  104 => 27,  102 => 26,  99 => 25,  96 => 24,  94 => 23,  91 => 22,  89 => 19,  83 => 16,  77 => 12,  75 => 7,  72 => 5,  69 => 4,  63 => 56,  61 => 55,  55 => 52,  51 => 50,  49 => 35,  42 => 33,  39 => 32,  37 => 4,  28 => 2,  25 => 1,);
    }

    /** @deprecated since 1.27 (to be removed in 2.0). Use getSourceContext() instead */
    public function getSource()
    {
        @trigger_error('The '.__METHOD__.' method is deprecated since version 1.27 and will be removed in 2.0. Use getSourceContext() instead.', E_USER_DEPRECATED);

        return $this->getSourceContext()->getCode();
    }

    public function getSourceContext()
    {
        return new Twig_Source("<!DOCTYPE html>
<html id=\"ng-app\" {% if language is defined %}lang=\"{{ language }}\"{% endif %} ng-app=\"piwikApp\">
    <head>
        {% block head %}
            <meta charset=\"utf-8\">
            <title>
                {%- block pageTitle %}
                    {%- if title is defined -%}{{ title }} - {% endif -%}
                    {%- if categoryTitle is defined -%}{{ categoryTitle }} - {% endif -%}
                    Matomo
                {%- endblock -%}
            </title>
            <meta http-equiv=\"X-UA-Compatible\" content=\"IE=EDGE,chrome=1\"/>
            <meta name=\"viewport\" content=\"initial-scale=1.0\"/>
            <meta name=\"generator\" content=\"Matomo - free/libre analytics platform\"/>
            <meta name=\"description\" content=\"{% block pageDescription %}{% endblock %}\"/>
            <meta name=\"apple-itunes-app\" content=\"app-id=737216887\" />
            <meta name=\"google\" content=\"notranslate\">
            {% block meta %}
                <meta name=\"robots\" content=\"noindex,nofollow\">
            {% endblock %}

            {% include \"@CoreHome/_favicon.twig\" %}
            {% include \"@CoreHome/_applePinnedTabIcon.twig\" %}
            <meta name=\"theme-color\" content=\"#3450A3\">
            {% include \"_jsGlobalVariables.twig\" %}
            {% include \"_jsCssIncludes.twig\" %}

            {%- if not isCustomLogo %}<link rel=\"manifest\" href=\"plugins/CoreHome/javascripts/manifest.json\">{% endif %}

        {% endblock %}
    </head>
    <body id=\"{{ bodyId|default('') }}\" ng-app=\"app\" class=\"{{ bodyClass|default('') }}\">

    {% block body %}

        {% include \"_iframeBuster.twig\" %}
        {% include \"@CoreHome/_javaScriptDisabled.twig\" %}

        <div id=\"root\">
            {% block root %}
            {% endblock %}
        </div>

        <div piwik-popover-handler></div>

        {% include \"@CoreHome/_shortcuts.twig\" %}

    {% endblock %}

        <div id=\"pageFooter\">
            {{ postEvent('Template.pageFooter') }}
        </div>

        {% include \"@CoreHome/_adblockDetect.twig\" %}
    </body>
</html>
", "layout.twig", "D:\\xampp\\htdocs\\matomo\\plugins\\Morpheus\\templates\\layout.twig");
    }
}
