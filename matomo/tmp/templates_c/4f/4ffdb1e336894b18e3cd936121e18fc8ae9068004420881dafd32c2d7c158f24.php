<?php

/* @CorePluginsAdmin/tagManagerTeaser.twig */
class __TwigTemplate_80fc1d2dd358a4ca242fc99202ea1e7e61ce8a8c19aa1a4943c33c22c7472305 extends Twig_Template
{
    public function __construct(Twig_Environment $env)
    {
        parent::__construct($env);

        // line 1
        $this->parent = $this->loadTemplate("dashboard.twig", "@CorePluginsAdmin/tagManagerTeaser.twig", 1);
        $this->blocks = array(
            'topcontrols' => array($this, 'block_topcontrols'),
            'content' => array($this, 'block_content'),
        );
    }

    protected function doGetParent(array $context)
    {
        return "dashboard.twig";
    }

    protected function doDisplay(array $context, array $blocks = array())
    {
        $this->parent->display($context, array_merge($this->blocks, $blocks));
    }

    // line 3
    public function block_topcontrols($context, array $blocks = array())
    {
    }

    // line 6
    public function block_content($context, array $blocks = array())
    {
        // line 7
        echo "<div class=\"activateTagManager\">
    <div class=\"row\">
        <div class=\"col s12\" style=\"text-align: center;\">
            <h2>";
        // line 10
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_TagManagerNowAvailableTitle")), "html", null, true);
        echo "</h2>
            <p>";
        // line 11
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_TagManagerNowAvailableSubtitle")), "html", null, true);
        echo "</p>
        </div>
    </div>
    ";
        // line 14
        ob_start();
        // line 15
        echo "    <div class=\"row\">
        <div class=\"col s12\">
            <div style=\"text-align: center;\">
                ";
        // line 18
        if (($context["isSuperUser"] ?? $this->getContext($context, "isSuperUser"))) {
            // line 19
            echo "                    <a href=\"";
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFunction('linkTo')->getCallable(), array(array("module" => "CorePluginsAdmin", "action" => "activate", "nonce" => ($context["nonce"] ?? $this->getContext($context, "nonce")), "pluginName" => "TagManager", "redirectTo" => "tagmanager"))), "html", null, true);
            echo "\"
                       class=\"btn activateTagManagerPlugin\"><span class=\"icon-rocket\"></span> ";
            // line 20
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_ActivateTagManagerNow")), "html", null, true);
            echo " <span class=\"icon-rocket\"></span></a>
                ";
        } else {
            // line 22
            echo "                    <a href=\"mailto:";
            echo \Piwik\piwik_escape_filter($this->env, \Piwik\piwik_escape_filter($this->env, ($context["superUserEmails"] ?? $this->getContext($context, "superUserEmails")), "url"), "html", null, true);
            echo "?subject=";
            echo \Piwik\piwik_escape_filter($this->env, \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_TagManagerNowAvailableTitle")), "url"), "html", null, true);
            echo "&body=";
            echo \Piwik\piwik_escape_filter($this->env, \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_TagManagerTeaserEmailSuperUserBody", "

", "

", ($context["piwikUrl"] ?? $this->getContext($context, "piwikUrl")), "

")), "url"), "html", null, true);
            echo "\"
                       class=\"btn activateTagManagerPlugin\"><span class=\"icon-rocket\"></span> ";
            // line 23
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_TagManagerEmailSuperUserToActivate")), "html", null, true);
            echo " <span class=\"icon-rocket\"></span></a>
                ";
        }
        // line 25
        echo "
                <a href=\"";
        // line 26
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFunction('linkTo')->getCallable(), array(array("module" => "CorePluginsAdmin", "action" => "disableActivateTagManagerPage"))), "html", null, true);
        echo "\"
                   class=\"btn dontShowAgainBtn\"><span class=\"icon-hide\"></span>
                    ";
        // line 28
        if (($context["isSuperUser"] ?? $this->getContext($context, "isSuperUser"))) {
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_TagManagerTeaserHideSuperUser")), "html", null, true);
        } else {
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_TagManagerTeaserHideNonSuperUser")), "html", null, true);
        }
        // line 29
        echo "                </a>
            </div>
        </div>
    </div>
    ";
        $context["actionBlock"] = ('' === $tmp = ob_get_clean()) ? '' : new Twig_Markup($tmp, $this->env->getCharset());
        // line 34
        echo "    ";
        echo ($context["actionBlock"] ?? $this->getContext($context, "actionBlock"));
        echo "
    <div class=\"row\">
        <div class=\"col ";
        // line 36
        if (($context["isSuperUser"] ?? $this->getContext($context, "isSuperUser"))) {
            echo "l4";
        } else {
            echo "l6";
        }
        echo " m12 s12\">
            <div piwik-content-block content-title=\"";
        // line 37
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_WhatIsTagManager")), "html", null, true);
        echo "\">
                <p>
                    ";
        // line 39
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_WhatIsTagManagerDetails1")), "html", null, true);
        echo "<br /><br />
                    ";
        // line 40
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_WhatIsTagManagerDetails2")), "html", null, true);
        echo "<br /><br />
                    <a href=\"https://matomo.org/docs/tag-manager\" rel=\"noreferrer noopener\">";
        // line 41
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_TagManagerLearnMoreInUserGuide")), "html", null, true);
        echo "</a>
                </p>
            </div>
        </div>
        <div class=\"col ";
        // line 45
        if (($context["isSuperUser"] ?? $this->getContext($context, "isSuperUser"))) {
            echo "l4";
        } else {
            echo "l6";
        }
        echo " m12 s12\">
            <div piwik-content-block content-title=\"";
        // line 46
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_WhyUsingATagManager")), "html", null, true);
        echo "\">
                <p>
                    ";
        // line 48
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_WhyUsingATagManagerDetails1")), "html", null, true);
        echo "
                    <br /><br />
                    ";
        // line 50
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_WhyUsingATagManagerDetails2")), "html", null, true);
        echo "
                    <br /><br />
                    ";
        // line 52
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_WhyUsingATagManagerDetails3")), "html", null, true);
        echo "
                    <br /><br /><br />
                    <a href=\"https://matomo.org/docs/tag-manager\" rel=\"noreferrer noopener\">";
        // line 54
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_TagManagerLearnMoreInUserGuide")), "html", null, true);
        echo "</a>
                </p>
            </div>
        </div>
        ";
        // line 58
        if (($context["isSuperUser"] ?? $this->getContext($context, "isSuperUser"))) {
            // line 59
            echo "            <div class=\"col l4 m12 s12\">
                <div piwik-content-block content-title=\"";
            // line 60
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_AreThereAnyRisks")), "html", null, true);
            echo "\">

                    ";
            // line 62
            echo call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_AreThereAnyRisksDetails1", "<a rel=\"noreferrer noopener\" href=\"https://en.wikipedia.org/wiki/Cross-site_scripting\">", "</a>"));
            echo "
                    <br /><br />
                    ";
            // line 64
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_AreThereAnyRisksDetails2")), "html", null, true);
            echo "
                    <br /><br /><br />
                    <a href=\"https://matomo.org/docs/tag-manager/#website-security\" rel=\"noreferrer noopener\">";
            // line 66
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_TagManagerLearnMoreInUserGuide")), "html", null, true);
            echo "</a>
                </div>
            </div>
        ";
        }
        // line 70
        echo "    </div>
    ";
        // line 71
        echo ($context["actionBlock"] ?? $this->getContext($context, "actionBlock"));
        echo "
</div>
";
    }

    public function getTemplateName()
    {
        return "@CorePluginsAdmin/tagManagerTeaser.twig";
    }

    public function isTraitable()
    {
        return false;
    }

    public function getDebugInfo()
    {
        return array (  211 => 71,  208 => 70,  201 => 66,  196 => 64,  191 => 62,  186 => 60,  183 => 59,  181 => 58,  174 => 54,  169 => 52,  164 => 50,  159 => 48,  154 => 46,  146 => 45,  139 => 41,  135 => 40,  131 => 39,  126 => 37,  118 => 36,  112 => 34,  105 => 29,  99 => 28,  94 => 26,  91 => 25,  86 => 23,  71 => 22,  66 => 20,  61 => 19,  59 => 18,  54 => 15,  52 => 14,  46 => 11,  42 => 10,  37 => 7,  34 => 6,  29 => 3,  11 => 1,);
    }

    /** @deprecated since 1.27 (to be removed in 2.0). Use getSourceContext() instead */
    public function getSource()
    {
        @trigger_error('The '.__METHOD__.' method is deprecated since version 1.27 and will be removed in 2.0. Use getSourceContext() instead.', E_USER_DEPRECATED);

        return $this->getSourceContext()->getCode();
    }

    public function getSourceContext()
    {
        return new Twig_Source("{% extends 'dashboard.twig' %}

{% block topcontrols %}
{% endblock %}

{% block content %}
<div class=\"activateTagManager\">
    <div class=\"row\">
        <div class=\"col s12\" style=\"text-align: center;\">
            <h2>{{ 'CorePluginsAdmin_TagManagerNowAvailableTitle'|translate }}</h2>
            <p>{{ 'CorePluginsAdmin_TagManagerNowAvailableSubtitle'|translate }}</p>
        </div>
    </div>
    {% set actionBlock %}
    <div class=\"row\">
        <div class=\"col s12\">
            <div style=\"text-align: center;\">
                {% if isSuperUser %}
                    <a href=\"{{ linkTo({'module': 'CorePluginsAdmin', 'action': 'activate', 'nonce': nonce, 'pluginName': 'TagManager', 'redirectTo': 'tagmanager'}) }}\"
                       class=\"btn activateTagManagerPlugin\"><span class=\"icon-rocket\"></span> {{ 'CorePluginsAdmin_ActivateTagManagerNow'|translate }} <span class=\"icon-rocket\"></span></a>
                {% else %}
                    <a href=\"mailto:{{ superUserEmails|e('url') }}?subject={{ 'CorePluginsAdmin_TagManagerNowAvailableTitle'|translate|e('url') }}&body={{ 'CorePluginsAdmin_TagManagerTeaserEmailSuperUserBody'|translate(\"\\n\\n\", \"\\n\\n\", piwikUrl, \"\\n\\n\")|e('url') }}\"
                       class=\"btn activateTagManagerPlugin\"><span class=\"icon-rocket\"></span> {{ 'CorePluginsAdmin_TagManagerEmailSuperUserToActivate'|translate }} <span class=\"icon-rocket\"></span></a>
                {% endif %}

                <a href=\"{{ linkTo({'module': 'CorePluginsAdmin', 'action': 'disableActivateTagManagerPage'}) }}\"
                   class=\"btn dontShowAgainBtn\"><span class=\"icon-hide\"></span>
                    {% if isSuperUser %}{{ 'CorePluginsAdmin_TagManagerTeaserHideSuperUser'|translate }}{% else %}{{ 'CorePluginsAdmin_TagManagerTeaserHideNonSuperUser'|translate }}{% endif %}
                </a>
            </div>
        </div>
    </div>
    {% endset %}
    {{ actionBlock|raw }}
    <div class=\"row\">
        <div class=\"col {% if isSuperUser %}l4{% else %}l6{% endif %} m12 s12\">
            <div piwik-content-block content-title=\"{{ 'CorePluginsAdmin_WhatIsTagManager'|translate }}\">
                <p>
                    {{ 'CorePluginsAdmin_WhatIsTagManagerDetails1'|translate }}<br /><br />
                    {{ 'CorePluginsAdmin_WhatIsTagManagerDetails2'|translate }}<br /><br />
                    <a href=\"https://matomo.org/docs/tag-manager\" rel=\"noreferrer noopener\">{{ 'CorePluginsAdmin_TagManagerLearnMoreInUserGuide'|translate }}</a>
                </p>
            </div>
        </div>
        <div class=\"col {% if isSuperUser %}l4{% else %}l6{% endif %} m12 s12\">
            <div piwik-content-block content-title=\"{{ 'CorePluginsAdmin_WhyUsingATagManager'|translate }}\">
                <p>
                    {{ 'CorePluginsAdmin_WhyUsingATagManagerDetails1'|translate }}
                    <br /><br />
                    {{ 'CorePluginsAdmin_WhyUsingATagManagerDetails2'|translate }}
                    <br /><br />
                    {{ 'CorePluginsAdmin_WhyUsingATagManagerDetails3'|translate }}
                    <br /><br /><br />
                    <a href=\"https://matomo.org/docs/tag-manager\" rel=\"noreferrer noopener\">{{ 'CorePluginsAdmin_TagManagerLearnMoreInUserGuide'|translate }}</a>
                </p>
            </div>
        </div>
        {% if isSuperUser %}
            <div class=\"col l4 m12 s12\">
                <div piwik-content-block content-title=\"{{ 'CorePluginsAdmin_AreThereAnyRisks'|translate }}\">

                    {{ 'CorePluginsAdmin_AreThereAnyRisksDetails1'|translate('<a rel=\"noreferrer noopener\" href=\"https://en.wikipedia.org/wiki/Cross-site_scripting\">', '</a>')|raw }}
                    <br /><br />
                    {{ 'CorePluginsAdmin_AreThereAnyRisksDetails2'|translate }}
                    <br /><br /><br />
                    <a href=\"https://matomo.org/docs/tag-manager/#website-security\" rel=\"noreferrer noopener\">{{ 'CorePluginsAdmin_TagManagerLearnMoreInUserGuide'|translate }}</a>
                </div>
            </div>
        {% endif %}
    </div>
    {{ actionBlock|raw }}
</div>
{% endblock %}
", "@CorePluginsAdmin/tagManagerTeaser.twig", "D:\\xampp\\htdocs\\matomo\\plugins\\CorePluginsAdmin\\templates\\tagManagerTeaser.twig");
    }
}
