<?php

/* @Referrers/_visitorDetails.twig */
class __TwigTemplate_26fc05041d07167a54e9dab23215a4dd7ffd6948ffba0bed9261e308ba07fd86 extends Twig_Template
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
        echo "<div class=\"visitorReferrer ";
        echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerType"), "method"), "html", null, true);
        echo "\">
    ";
        // line 2
        if (($this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerType"), "method") == "website")) {
            // line 3
            echo "        <span>";
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Referrers_ColumnWebsite")), "html", null, true);
            echo ":</span>
        <a href=\"";
            // line 4
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('safelink')->getCallable(), array($this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerUrl"), "method"))), "html_attr");
            echo "\" rel=\"noreferrer noopener\" target=\"_blank\" class=\"visitorLogTooltip\" title=\"";
            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerUrl"), "method"), "html", null, true);
            echo "\"
           style=\"text-decoration:underline;\">
            ";
            // line 6
            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerName"), "method"), "html", null, true);
            echo "
        </a>
    ";
        }
        // line 9
        echo "    ";
        if (($this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerType"), "method") == "campaign")) {
            // line 10
            echo "        <span>";
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Referrers_ColumnCampaign")), "html", null, true);
            echo ": ";
            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerName"), "method"), "html", null, true);
            echo "
            ";
            // line 11
            if ( !twig_test_empty($this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerKeyword"), "method"))) {
                echo " - ";
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerKeyword"), "method"), "html", null, true);
            }
            echo "</span>
    ";
        }
        // line 13
        echo "    ";
        if (($this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerType"), "method") == "social")) {
            // line 14
            echo "        <span>";
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Referrers_ColumnSocial")), "html", null, true);
            echo ":</span>
        ";
            // line 15
            if ($this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerSearchEngineIcon"), "method")) {
                // line 16
                echo "            <img width=\"16\" src=\"";
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerSocialNetworkIcon"), "method"), "html", null, true);
                echo "\" alt=\"";
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerName"), "method"), "html", null, true);
                echo "\"/>
        ";
            }
            // line 18
            echo "        <a href=\"";
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('safelink')->getCallable(), array($this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerUrl"), "method"))), "html_attr");
            echo "\" rel=\"noreferrer\" target=\"_blank\" class=\"visitorLogTooltip\" title=\"";
            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerUrl"), "method"), "html", null, true);
            echo "\"
           style=\"text-decoration:underline;\">
            ";
            // line 20
            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerName"), "method"), "html", null, true);
            echo "
        </a>
    ";
        }
        // line 23
        echo "    ";
        if (($this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerType"), "method") == "search")) {
            // line 24
            $context["keywordNotDefined"] = call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_NotDefined", call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_ColumnKeyword"))));
            // line 25
            $context["showKeyword"] = ( !twig_test_empty($this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerKeyword"), "method")) && ($this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerKeyword"), "method") != ($context["keywordNotDefined"] ?? $this->getContext($context, "keywordNotDefined"))));
            // line 26
            echo "<span ";
            if ( !($context["showKeyword"] ?? $this->getContext($context, "showKeyword"))) {
                echo "title=\"";
                echo \Piwik\piwik_escape_filter($this->env, ($context["keywordNotDefined"] ?? $this->getContext($context, "keywordNotDefined")), "html", null, true);
                echo "\"";
            }
            echo ">
        ";
            // line 27
            if ($this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerSearchEngineIcon"), "method")) {
                // line 28
                echo "            <img width=\"16\" src=\"";
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerSearchEngineIcon"), "method"), "html", null, true);
                echo "\" alt=\"";
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerName"), "method"), "html", null, true);
                echo "\"/>
        ";
            }
            // line 30
            echo "            <span>";
            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerName"), "method"), "html", null, true);
            echo "</span>";
            // line 31
            if (($context["showKeyword"] ?? $this->getContext($context, "showKeyword"))) {
                echo "<span>, ";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Referrers_Keywords")), "html", null, true);
                echo ":</span>
            <a href=\"";
                // line 32
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('safelink')->getCallable(), array($this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerUrl"), "method"))), "html_attr");
                echo "\" rel=\"noreferrer noopener\" target=\"_blank\" style=\"text-decoration:underline;\">
                \"";
                // line 33
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerKeyword"), "method"), "html", null, true);
                echo "\"</a>
        ";
            }
            // line 35
            echo "        ";
            ob_start();
            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerKeyword"), "method"), "html", null, true);
            $context["keyword"] = ('' === $tmp = ob_get_clean()) ? '' : new Twig_Markup($tmp, $this->env->getCharset());
            // line 36
            echo "        ";
            ob_start();
            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerName"), "method"), "html", null, true);
            $context["searchName"] = ('' === $tmp = ob_get_clean()) ? '' : new Twig_Markup($tmp, $this->env->getCharset());
            // line 37
            echo "        ";
            ob_start();
            echo "#";
            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerKeywordPosition"), "method"), "html", null, true);
            $context["position"] = ('' === $tmp = ob_get_clean()) ? '' : new Twig_Markup($tmp, $this->env->getCharset());
            // line 38
            echo "        ";
            if ($this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerKeywordPosition"), "method")) {
                // line 39
                echo "            <span title='";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Live_KeywordRankedOnSearchResultForThisVisitor", ($context["keyword"] ?? $this->getContext($context, "keyword")), ($context["position"] ?? $this->getContext($context, "position")), ($context["searchName"] ?? $this->getContext($context, "searchName")))), "html", null, true);
                echo "' class='visitorRank visitorLogTooltip'>
                            <span class='hash'>#</span>
                ";
                // line 41
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerKeywordPosition"), "method"), "html", null, true);
                echo "
                        </span>
        ";
            }
            // line 44
            echo "        </span>
    ";
        }
        // line 46
        echo "    ";
        if (($this->getAttribute(($context["visitInfo"] ?? $this->getContext($context, "visitInfo")), "getColumn", array(0 => "referrerType"), "method") == "direct")) {
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Referrers_DirectEntry")), "html", null, true);
        }
        // line 47
        echo "</div>
";
    }

    public function getTemplateName()
    {
        return "@Referrers/_visitorDetails.twig";
    }

    public function isTraitable()
    {
        return false;
    }

    public function getDebugInfo()
    {
        return array (  179 => 47,  174 => 46,  170 => 44,  164 => 41,  158 => 39,  155 => 38,  149 => 37,  144 => 36,  139 => 35,  134 => 33,  130 => 32,  124 => 31,  120 => 30,  112 => 28,  110 => 27,  101 => 26,  99 => 25,  97 => 24,  94 => 23,  88 => 20,  80 => 18,  72 => 16,  70 => 15,  65 => 14,  62 => 13,  54 => 11,  47 => 10,  44 => 9,  38 => 6,  31 => 4,  26 => 3,  24 => 2,  19 => 1,);
    }

    /** @deprecated since 1.27 (to be removed in 2.0). Use getSourceContext() instead */
    public function getSource()
    {
        @trigger_error('The '.__METHOD__.' method is deprecated since version 1.27 and will be removed in 2.0. Use getSourceContext() instead.', E_USER_DEPRECATED);

        return $this->getSourceContext()->getCode();
    }

    public function getSourceContext()
    {
        return new Twig_Source("<div class=\"visitorReferrer {{ visitInfo.getColumn('referrerType') }}\">
    {% if visitInfo.getColumn('referrerType') == 'website' %}
        <span>{{ 'Referrers_ColumnWebsite'|translate }}:</span>
        <a href=\"{{ visitInfo.getColumn('referrerUrl')|safelink|e('html_attr') }}\" rel=\"noreferrer noopener\" target=\"_blank\" class=\"visitorLogTooltip\" title=\"{{ visitInfo.getColumn('referrerUrl') }}\"
           style=\"text-decoration:underline;\">
            {{ visitInfo.getColumn('referrerName') }}
        </a>
    {% endif %}
    {% if visitInfo.getColumn('referrerType') == 'campaign' %}
        <span>{{ 'Referrers_ColumnCampaign'|translate }}: {{ visitInfo.getColumn('referrerName') }}
            {% if visitInfo.getColumn('referrerKeyword') is not empty %} - {{ visitInfo.getColumn('referrerKeyword') }}{% endif %}</span>
    {% endif %}
    {% if visitInfo.getColumn('referrerType') == 'social' %}
        <span>{{ 'Referrers_ColumnSocial'|translate }}:</span>
        {% if visitInfo.getColumn('referrerSearchEngineIcon') %}
            <img width=\"16\" src=\"{{ visitInfo.getColumn('referrerSocialNetworkIcon') }}\" alt=\"{{ visitInfo.getColumn('referrerName') }}\"/>
        {% endif %}
        <a href=\"{{ visitInfo.getColumn('referrerUrl')|safelink|e('html_attr') }}\" rel=\"noreferrer\" target=\"_blank\" class=\"visitorLogTooltip\" title=\"{{ visitInfo.getColumn('referrerUrl') }}\"
           style=\"text-decoration:underline;\">
            {{ visitInfo.getColumn('referrerName') }}
        </a>
    {% endif %}
    {% if visitInfo.getColumn('referrerType') == 'search' %}
        {%- set keywordNotDefined = 'General_NotDefined'|translate('General_ColumnKeyword'|translate) -%}
        {%- set showKeyword = visitInfo.getColumn('referrerKeyword') is not empty and visitInfo.getColumn('referrerKeyword') != keywordNotDefined -%}
        <span {% if not showKeyword %}title=\"{{ keywordNotDefined }}\"{% endif %}>
        {% if visitInfo.getColumn('referrerSearchEngineIcon') %}
            <img width=\"16\" src=\"{{ visitInfo.getColumn('referrerSearchEngineIcon') }}\" alt=\"{{ visitInfo.getColumn('referrerName') }}\"/>
        {% endif %}
            <span>{{ visitInfo.getColumn('referrerName') }}</span>
        {%- if showKeyword %}<span>, {{ 'Referrers_Keywords'|translate }}:</span>
            <a href=\"{{ visitInfo.getColumn('referrerUrl')|safelink|e('html_attr') }}\" rel=\"noreferrer noopener\" target=\"_blank\" style=\"text-decoration:underline;\">
                \"{{ visitInfo.getColumn('referrerKeyword') }}\"</a>
        {% endif %}
        {% set keyword %}{{ visitInfo.getColumn('referrerKeyword') }}{% endset %}
        {% set searchName %}{{ visitInfo.getColumn('referrerName') }}{% endset %}
        {% set position %}#{{ visitInfo.getColumn('referrerKeywordPosition') }}{% endset %}
        {% if visitInfo.getColumn('referrerKeywordPosition') %}
            <span title='{{ 'Live_KeywordRankedOnSearchResultForThisVisitor'|translate(keyword,position,searchName) }}' class='visitorRank visitorLogTooltip'>
                            <span class='hash'>#</span>
                {{ visitInfo.getColumn('referrerKeywordPosition') }}
                        </span>
        {% endif %}
        </span>
    {% endif %}
    {% if visitInfo.getColumn('referrerType') == 'direct' %}{{ 'Referrers_DirectEntry'|translate }}{% endif %}
</div>
", "@Referrers/_visitorDetails.twig", "D:\\xampp\\htdocs\\matomo\\plugins\\Referrers\\templates\\_visitorDetails.twig");
    }
}
