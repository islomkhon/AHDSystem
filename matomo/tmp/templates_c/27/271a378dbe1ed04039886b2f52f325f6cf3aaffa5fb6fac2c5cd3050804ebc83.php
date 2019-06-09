<?php

/* @Live/_actionCommon.twig */
class __TwigTemplate_aa1c6ea0829c025c10bb0b55cbad86c9bfcdec00583b866c252b1a27e2c423cf extends Twig_Template
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
        echo "<li class=\"";
        if ($this->getAttribute(($context["action"] ?? null), "goalName", array(), "any", true, true)) {
            echo "goal";
        } else {
            echo "action";
        }
        echo "\"
    title=\"";
        // line 2
        echo call_user_func_array($this->env->getFunction('postEvent')->getCallable(), array("Live.renderActionTooltip", ($context["action"] ?? $this->getContext($context, "action")), ($context["visitInfo"] ?? $this->getContext($context, "visitInfo"))));
        echo "\">
    <div>
        ";
        // line 5
        echo "        ";
        if ( !twig_test_empty((($this->getAttribute(($context["action"] ?? null), "pageTitle", array(), "any", true, true)) ? (_twig_default_filter($this->getAttribute(($context["action"] ?? null), "pageTitle", array()), false)) : (false)))) {
            // line 6
            echo "            <span class=\"truncated-text-line\">";
            echo call_user_func_array($this->env->getFilter('rawSafeDecoded')->getCallable(), array($this->getAttribute(($context["action"] ?? $this->getContext($context, "action")), "pageTitle", array())));
            echo "</span>
        ";
        }
        // line 8
        echo "        ";
        if ($this->getAttribute(($context["action"] ?? null), "siteSearchKeyword", array(), "any", true, true)) {
            // line 9
            echo "            ";
            if (($this->getAttribute(($context["action"] ?? $this->getContext($context, "action")), "type", array()) == "search")) {
                // line 10
                echo "                <img src='";
                echo \Piwik\piwik_escape_filter($this->env, (($this->getAttribute(($context["action"] ?? null), "iconSVG", array(), "any", true, true)) ? (_twig_default_filter($this->getAttribute(($context["action"] ?? null), "iconSVG", array()), $this->getAttribute(($context["action"] ?? $this->getContext($context, "action")), "icon", array()))) : ($this->getAttribute(($context["action"] ?? $this->getContext($context, "action")), "icon", array()))), "html", null, true);
                echo "' title='";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Actions_SubmenuSitesearch")), "html", null, true);
                echo "'
                     class=\"action-list-action-icon search\">
            ";
            }
            // line 13
            echo "            <span class=\"truncated-text-line\">";
            echo call_user_func_array($this->env->getFilter('rawSafeDecoded')->getCallable(), array($this->getAttribute(($context["action"] ?? $this->getContext($context, "action")), "siteSearchKeyword", array())));
            echo "</span>
        ";
        }
        // line 15
        echo "        ";
        if ( !twig_test_empty($this->getAttribute(($context["action"] ?? $this->getContext($context, "action")), "url", array()))) {
            // line 16
            echo "            ";
            if ( !twig_test_empty((($this->getAttribute(($context["action"] ?? null), "iconSVG", array(), "any", true, true)) ? (_twig_default_filter($this->getAttribute(($context["action"] ?? null), "iconSVG", array()), $this->getAttribute(($context["action"] ?? $this->getContext($context, "action")), "icon", array()))) : ($this->getAttribute(($context["action"] ?? $this->getContext($context, "action")), "icon", array()))))) {
                // line 17
                echo "                <img src='";
                echo \Piwik\piwik_escape_filter($this->env, (($this->getAttribute(($context["action"] ?? null), "iconSVG", array(), "any", true, true)) ? (_twig_default_filter($this->getAttribute(($context["action"] ?? null), "iconSVG", array()), $this->getAttribute(($context["action"] ?? $this->getContext($context, "action")), "icon", array()))) : ($this->getAttribute(($context["action"] ?? $this->getContext($context, "action")), "icon", array()))), "html", null, true);
                echo "' class=\"action-list-action-icon ";
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["action"] ?? $this->getContext($context, "action")), "type", array()), "html", null, true);
                echo "\">
            ";
            }
            // line 19
            echo "            ";
            if ((($this->getAttribute(($context["action"] ?? $this->getContext($context, "action")), "type", array()) == "action") &&  !twig_test_empty((($this->getAttribute(($context["action"] ?? null), "pageTitle", array(), "any", true, true)) ? (_twig_default_filter($this->getAttribute(($context["action"] ?? null), "pageTitle", array()), false)) : (false))))) {
                echo "<p>";
            }
            // line 20
            echo "            ";
            if ((((is_string($__internal_7cd7461123377b8c9c1b6a01f46c7bbd94bd12e59266005df5e93029ddbc0ec5 = twig_lower_filter($this->env, twig_trim_filter($this->getAttribute(($context["action"] ?? $this->getContext($context, "action")), "url", array())))) && is_string($__internal_3e28b7f596c58d7729642bcf2acc6efc894803703bf5fa7e74cd8d2aa1f8c68a = "javascript:") && ('' === $__internal_3e28b7f596c58d7729642bcf2acc6efc894803703bf5fa7e74cd8d2aa1f8c68a || 0 === strpos($__internal_7cd7461123377b8c9c1b6a01f46c7bbd94bd12e59266005df5e93029ddbc0ec5, $__internal_3e28b7f596c58d7729642bcf2acc6efc894803703bf5fa7e74cd8d2aa1f8c68a))) || (is_string($__internal_b0b3d6199cdf4d15a08b3fb98fe017ecb01164300193d18d78027218d843fc57 = twig_lower_filter($this->env, twig_trim_filter($this->getAttribute(            // line 21
($context["action"] ?? $this->getContext($context, "action")), "url", array())))) && is_string($__internal_81ccf322d0988ca0aa9ae9943d772c435c5ff01fb50b956278e245e40ae66ab9 = "vbscript:") && ('' === $__internal_81ccf322d0988ca0aa9ae9943d772c435c5ff01fb50b956278e245e40ae66ab9 || 0 === strpos($__internal_b0b3d6199cdf4d15a08b3fb98fe017ecb01164300193d18d78027218d843fc57, $__internal_81ccf322d0988ca0aa9ae9943d772c435c5ff01fb50b956278e245e40ae66ab9)))) || (is_string($__internal_add9db1f328aaed12ef1a33890510da978cc9cf3e50f6769d368473a9c90c217 = twig_lower_filter($this->env, twig_trim_filter($this->getAttribute(            // line 22
($context["action"] ?? $this->getContext($context, "action")), "url", array())))) && is_string($__internal_128c19eb75d89ae9acc1294da2e091b433005202cb9b9351ea0c5dd5f69ee105 = "data:") && ('' === $__internal_128c19eb75d89ae9acc1294da2e091b433005202cb9b9351ea0c5dd5f69ee105 || 0 === strpos($__internal_add9db1f328aaed12ef1a33890510da978cc9cf3e50f6769d368473a9c90c217, $__internal_128c19eb75d89ae9acc1294da2e091b433005202cb9b9351ea0c5dd5f69ee105))))) {
                // line 23
                echo "                ";
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["action"] ?? $this->getContext($context, "action")), "url", array()), "html", null, true);
                echo "
            ";
            } else {
                // line 25
                echo "                <a href=\"";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('safelink')->getCallable(), array($this->getAttribute(($context["action"] ?? $this->getContext($context, "action")), "url", array()))), "html_attr");
                echo "\" rel=\"noreferrer noopener\" target=\"_blank\"
                   class=\"action-list-url truncated-text-line\">
                    ";
                // line 27
                echo \Piwik\piwik_escape_filter($this->env, twig_replace_filter($this->getAttribute(($context["action"] ?? $this->getContext($context, "action")), "url", array()), array("http://" => "", "https://" => "")), "html", null, true);
                echo "
                </a>
            ";
            }
            // line 30
            echo "            ";
            if ((($this->getAttribute(($context["action"] ?? $this->getContext($context, "action")), "type", array()) == "action") &&  !twig_test_empty((($this->getAttribute(($context["action"] ?? null), "pageTitle", array(), "any", true, true)) ? (_twig_default_filter($this->getAttribute(($context["action"] ?? null), "pageTitle", array()), false)) : (false))))) {
                echo "</p>";
            }
            // line 31
            echo "        ";
        } elseif (($this->getAttribute(($context["action"] ?? $this->getContext($context, "action")), "type", array()) != "search")) {
            // line 32
            echo "            <p>
                <span>";
            // line 33
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_NotDefined", call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Actions_ColumnPageURL")))), "html", null, true);
            echo "</span>
            </p>
        ";
        }
        // line 36
        echo "    </div>
</li>
";
    }

    public function getTemplateName()
    {
        return "@Live/_actionCommon.twig";
    }

    public function isTraitable()
    {
        return false;
    }

    public function getDebugInfo()
    {
        return array (  122 => 36,  116 => 33,  113 => 32,  110 => 31,  105 => 30,  99 => 27,  93 => 25,  87 => 23,  85 => 22,  84 => 21,  82 => 20,  77 => 19,  69 => 17,  66 => 16,  63 => 15,  57 => 13,  48 => 10,  45 => 9,  42 => 8,  36 => 6,  33 => 5,  28 => 2,  19 => 1,);
    }

    /** @deprecated since 1.27 (to be removed in 2.0). Use getSourceContext() instead */
    public function getSource()
    {
        @trigger_error('The '.__METHOD__.' method is deprecated since version 1.27 and will be removed in 2.0. Use getSourceContext() instead.', E_USER_DEPRECATED);

        return $this->getSourceContext()->getCode();
    }

    public function getSourceContext()
    {
        return new Twig_Source("<li class=\"{% if action.goalName is defined %}goal{% else %}action{% endif %}\"
    title=\"{{ postEvent('Live.renderActionTooltip', action, visitInfo) }}\">
    <div>
        {# Page view / Download / Outlink #}
        {% if action.pageTitle|default(false) is not empty %}
            <span class=\"truncated-text-line\">{{ action.pageTitle|rawSafeDecoded }}</span>
        {% endif %}
        {% if action.siteSearchKeyword is defined %}
            {% if action.type == 'search' %}
                <img src='{{ action.iconSVG|default(action.icon) }}' title='{{ 'Actions_SubmenuSitesearch'|translate }}'
                     class=\"action-list-action-icon search\">
            {% endif %}
            <span class=\"truncated-text-line\">{{ action.siteSearchKeyword|rawSafeDecoded }}</span>
        {% endif %}
        {% if action.url is not empty %}
            {% if action.iconSVG|default(action.icon) is not empty %}
                <img src='{{ action.iconSVG|default(action.icon) }}' class=\"action-list-action-icon {{ action.type }}\">
            {% endif %}
            {% if action.type == 'action' and action.pageTitle|default(false) is not empty %}<p>{% endif %}
            {% if action.url|trim|lower starts with 'javascript:' or
            action.url|trim|lower starts with 'vbscript:' or
            action.url|trim|lower starts with 'data:' %}
                {{ action.url }}
            {% else %}
                <a href=\"{{ action.url|safelink|e('html_attr') }}\" rel=\"noreferrer noopener\" target=\"_blank\"
                   class=\"action-list-url truncated-text-line\">
                    {{ action.url|replace({'http://': '', 'https://': ''}) }}
                </a>
            {% endif %}
            {% if action.type == 'action' and action.pageTitle|default(false) is not empty %}</p>{% endif %}
        {% elseif action.type != 'search' %}
            <p>
                <span>{{ 'General_NotDefined'|translate('Actions_ColumnPageURL'|translate) }}</span>
            </p>
        {% endif %}
    </div>
</li>
", "@Live/_actionCommon.twig", "D:\\xampp\\htdocs\\matomo\\plugins\\Live\\templates\\_actionCommon.twig");
    }
}
