<?php

/* @Marketplace/plugin-list.twig */
class __TwigTemplate_e5c947cb1abc6a6cd58b54d995bae330825dfd504d72c8c7aadd2b1dd9a16756 extends Twig_Template
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
        $context["marketplaceMacro"] = $this->loadTemplate("@Marketplace/macros.twig", "@Marketplace/plugin-list.twig", 1);
        // line 2
        echo "
";
        // line 3
        if ((twig_length_filter($this->env, ($context["pluginsToShow"] ?? $this->getContext($context, "pluginsToShow"))) > 0)) {
            // line 4
            echo "    <div class=\"pluginListContainer row\">
        ";
            // line 5
            $context['_parent'] = $context;
            $context['_seq'] = twig_ensure_traversable(($context["pluginsToShow"] ?? $this->getContext($context, "pluginsToShow")));
            $context['loop'] = array(
              'parent' => $context['_parent'],
              'index0' => 0,
              'index'  => 1,
              'first'  => true,
            );
            if (is_array($context['_seq']) || (is_object($context['_seq']) && $context['_seq'] instanceof Countable)) {
                $length = count($context['_seq']);
                $context['loop']['revindex0'] = $length - 1;
                $context['loop']['revindex'] = $length;
                $context['loop']['length'] = $length;
                $context['loop']['last'] = 1 === $length;
            }
            foreach ($context['_seq'] as $context["_key"] => $context["plugin"]) {
                // line 6
                echo "            <div class=\"col s12 m6 l4\">
                ";
                // line 7
                $this->loadTemplate("@Marketplace/plugin-list.twig", "@Marketplace/plugin-list.twig", 7, "488967171")->display(array_merge($context, array("title" => "")));
                // line 152
                echo "            </div>
        ";
                ++$context['loop']['index0'];
                ++$context['loop']['index'];
                $context['loop']['first'] = false;
                if (isset($context['loop']['length'])) {
                    --$context['loop']['revindex0'];
                    --$context['loop']['revindex'];
                    $context['loop']['last'] = 0 === $context['loop']['revindex0'];
                }
            }
            $_parent = $context['_parent'];
            unset($context['_seq'], $context['_iterated'], $context['_key'], $context['plugin'], $context['_parent'], $context['loop']);
            $context = array_intersect_key($context, $_parent) + $_parent;
            // line 154
            echo "    </div>
";
        }
        // line 156
        echo "
";
        // line 157
        if ((twig_length_filter($this->env, ($context["pluginsToShow"] ?? $this->getContext($context, "pluginsToShow"))) == 0)) {
            // line 158
            echo "    <div piwik-content-block>
        ";
            // line 159
            if (($context["showThemes"] ?? $this->getContext($context, "showThemes"))) {
                // line 160
                echo "            ";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_NoThemesFound")), "html", null, true);
                echo "
        ";
            } else {
                // line 162
                echo "            ";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_NoPluginsFound")), "html", null, true);
                echo "
        ";
            }
            // line 164
            echo "    </div>
";
        }
        // line 166
        echo "
";
    }

    public function getTemplateName()
    {
        return "@Marketplace/plugin-list.twig";
    }

    public function isTraitable()
    {
        return false;
    }

    public function getDebugInfo()
    {
        return array (  96 => 166,  92 => 164,  86 => 162,  80 => 160,  78 => 159,  75 => 158,  73 => 157,  70 => 156,  66 => 154,  51 => 152,  49 => 7,  46 => 6,  29 => 5,  26 => 4,  24 => 3,  21 => 2,  19 => 1,);
    }

    /** @deprecated since 1.27 (to be removed in 2.0). Use getSourceContext() instead */
    public function getSource()
    {
        @trigger_error('The '.__METHOD__.' method is deprecated since version 1.27 and will be removed in 2.0. Use getSourceContext() instead.', E_USER_DEPRECATED);

        return $this->getSourceContext()->getCode();
    }

    public function getSourceContext()
    {
        return new Twig_Source("{% import '@Marketplace/macros.twig' as marketplaceMacro %}

{% if pluginsToShow|length > 0 %}
    <div class=\"pluginListContainer row\">
        {% for plugin in pluginsToShow %}
            <div class=\"col s12 m6 l4\">
                {% embed 'contentBlock.twig' with {'title': ''} %}
                    {% block content %}
                        <div class=\"plugin\">
                            <h3 class=\"card-title\" title=\"{{ 'General_MoreDetails'|translate }}\">
                                <a href=\"#\" piwik-plugin-name=\"{{ plugin.name }}\">{{ plugin.displayName }}</a>
                            </h3>

                            <p class=\"description\">
                                {{ plugin.description }}
                                <a class=\"more\" href=\"#\" piwik-plugin-name=\"{{ plugin.name }}\" title=\"{{ 'General_MoreDetails'|translate }}\">
                                    &rsaquo; {{ 'General_MoreLowerCase'|translate }}</a>
                            </p>

                            {% if showThemes %}
                                {# Screenshot for themes #}
                                <a class=\"more\" href=\"#\" piwik-plugin-name=\"{{ plugin.name }}\">
                                    <img title=\"{{ 'General_MoreDetails'|translate }}\"
                                         class=\"preview\" src=\"{{ plugin.screenshots|first }}?w=250&h=150\"/></a>
                            {% endif %}

                            <ul class=\"metadata\">
                                {% if plugin.isBundle is not defined or not plugin.isBundle %}
                                    <li>
                                        {% if plugin.latestVersion %}
                                            {{ 'CorePluginsAdmin_Version'|translate }}: {{ plugin.latestVersion }}
                                        {% endif %}

                                        {% if plugin.canBeUpdated %}
                                            <a class=\"update-available\"
                                                {% if plugin.changelog is defined and plugin.changelog and plugin.changelog.url is defined and plugin.changelog.url %}
                                                    target=\"_blank\" href=\"{{ plugin.changelog.url|e('html_attr') }}\"
                                                {% else %}
                                                    href=\"#\" piwik-plugin-name=\"{{ plugin.name }}\"
                                                {% endif %}
                                               title=\"{{ 'Marketplace_PluginUpdateAvailable'|translate(plugin.currentVersion, plugin.latestVersion) }}\">
                                                {{ 'Marketplace_NewVersion'|translate }}</a>
                                        {% endif %}
                                    </li>
                                    {% if plugin.lastUpdated %}
                                        <li>{{ 'Marketplace_Updated'|translate }}: {{ plugin.lastUpdated }}</li>
                                    {% endif %}
                                    {% if plugin.numDownloads %}
                                        <li>{{ 'General_Downloads'|translate }}: {{ plugin.numDownloads }}</li>
                                    {% endif %}
                                    <li>{{ 'Marketplace_Developer'|translate }}: {{ marketplaceMacro.pluginDeveloper(plugin.owner) }}</li>
                                {% endif %}
                            </ul>

                            {% macro moreDetailsLink(plugin) %}
                                {% set canBePurchased = not plugin.isDownloadable and plugin.shop is defined and plugin.shop and plugin.shop.url %}
                                <a class=\"btn btn-block plugin-details {% if canBePurchased %}purchaseable{% endif %}\" href=\"#\" piwik-plugin-name=\"{{ plugin.name }}\" title=\"{{ 'General_MoreDetails'|translate }}\">

                                    {% if canBePurchased and plugin.shop.variations %}
                                        {% set foundCheapest = 0 %}
                                        {% for variation in plugin.shop.variations %}
                                            {% if not foundCheapest and variation.cheapest is defined and variation.cheapest %}
                                                {% set foundCheapest = 1 %}
                                                {{ 'Marketplace_PriceFromPerPeriod'|translate(variation.prettyPrice, variation.period) }}
                                            {% endif %}
                                        {% endfor %}
                                        {% if not foundCheapest %}
                                            {{ 'Marketplace_PriceFromPerPeriod'|translate(plugin.shop.variations.0.prettyPrice, plugin.shop.variations.0.period) }}
                                        {% endif %}
                                    {% else %}
                                        {{ 'General_MoreDetails'|translate }}
                                    {% endif %}

                                </a>
                            {% endmacro %}


                            {% if isSuperUser %}
                                <div class=\"footer\">
                                    {% if plugin.isMissingLicense is defined and plugin.isMissingLicense %}

                                        <div class=\"alert alert-danger\" >
                                            {{ 'Marketplace_LicenseMissing'|translate }}

                                            <span style=\"white-space:nowrap\">(<a class=\"plugin-details\" href=\"#\" piwik-plugin-name=\"{{ plugin.name }}\" title=\"{{ 'General_MoreDetails'|translate }}\">{{ 'General_Help'|translate }}</a>)</span>
                                        </div>

                                    {% elseif plugin.hasExceededLicense is defined and plugin.hasExceededLicense %}

                                        <div class=\"alert alert-danger\">
                                            {{ 'Marketplace_LicenseExceeded'|translate }}

                                            <span style=\"white-space:nowrap\">(<a class=\"plugin-details\" href=\"#\" piwik-plugin-name=\"{{ plugin.name }}\" title=\"{{ 'General_MoreDetails'|translate }}\">{{ 'General_Help'|translate }}</a>)</span>
                                        </div>

                                    {% elseif plugin.canBeUpdated and 0 == plugin.missingRequirements|length and isAutoUpdatePossible %}
                                        <a class=\"btn btn-block\"
                                           href=\"{{ linkTo({'module': 'Marketplace', 'action':'updatePlugin', 'pluginName': plugin.name, 'nonce': updateNonce}) }}\">
                                            {{ 'CoreUpdater_UpdateTitle'|translate }}
                                        </a>
                                    {% elseif plugin.missingRequirements|length > 0 or not isAutoUpdatePossible %}

                                        {% macro downloadButton(showOr, plugin, isAutoUpdatePossible, showBrackets = false) -%}
                                            {%- if plugin.missingRequirements|length == 0 and plugin.isDownloadable and not isAutoUpdatePossible -%}
                                                {% if showBrackets %}({% endif %}<span onclick=\"\$(this).css('display', 'none')\">
                                                {%- if showOr %} {{ 'General_Or'|translate }} {% endif -%}
                                                <a class=\"plugin-details download\"
                                                   href=\"{{ linkTo({'module': 'Marketplace', 'action': 'download', 'pluginName': plugin.name, 'nonce': (plugin.name|nonce)}) }}\"
                                                >{{ 'General_Download'|translate }}</a></span>{% if showBrackets %}){% endif %}
                                            {%- endif -%}
                                        {%- endmacro %}

                                        {% if plugin.canBeUpdated and 0 == plugin.missingRequirements|length %}
                                            {{ 'Marketplace_CannotUpdate'|translate }}
                                            <span style=\"white-space:nowrap\">(<a class=\"plugin-details\" href=\"#\" piwik-plugin-name=\"{{ plugin.name }}\" title=\"{{ 'General_MoreDetails'|translate }}\">{{ 'General_Help'|translate }}</a>{{ _self.downloadButton(true, plugin, isAutoUpdatePossible)|raw }})</span>
                                        {% elseif plugin.isInstalled %}
                                            {{ 'General_Installed'|translate }}
                                            {{ _self.downloadButton(false, plugin, isAutoUpdatePossible, true)|raw }}
                                        {% elseif not plugin.isDownloadable %}
                                            {{ _self.moreDetailsLink(plugin)|raw }}
                                        {% else %}
                                            {{ 'Marketplace_CannotInstall'|translate }}

                                            <span style=\"white-space:nowrap\">(<a class=\"plugin-details\" href=\"#\" piwik-plugin-name=\"{{ plugin.name }}\" title=\"{{ 'General_MoreDetails'|translate }}\">{{ 'General_Help'|translate }}</a>{{ _self.downloadButton(true, plugin, isAutoUpdatePossible)|raw }})</span>
                                        {% endif %}

                                    {% elseif plugin.isInstalled %}
                                        {{ 'General_Installed'|translate }}

                                        {% if not plugin.isInvalid and not isMultiServerEnvironment and isPluginsAdminEnabled %}
                                            ({{ pluginsMacro.pluginActivateDeactivateAction(plugin.name, plugin.isActivated, plugin.missingRequirements, deactivateNonce, activateNonce) }})
                                        {% endif %}

                                    {% elseif plugin.isPaid and not plugin.isDownloadable %}
                                        {{ _self.moreDetailsLink(plugin)|raw }}
                                    {% else %}
                                        <a href=\"{{ linkTo({'module': 'Marketplace', 'action': 'installPlugin', 'pluginName': plugin.name, 'nonce': installNonce}) }}\"
                                           class=\"btn\">
                                            {{ 'Marketplace_ActionInstall'|translate }}
                                        </a>
                                    {% endif %}
                                </div>
                            {% else %}
                                <div class=\"footer\">
                                    {{ _self.moreDetailsLink(plugin)|raw }}
                                </div>
                            {% endif %}

                        </div>
                    {% endblock %}
                {% endembed %}
            </div>
        {% endfor %}
    </div>
{% endif %}

{% if pluginsToShow|length == 0 %}
    <div piwik-content-block>
        {% if showThemes %}
            {{ 'Marketplace_NoThemesFound'|translate }}
        {% else %}
            {{ 'Marketplace_NoPluginsFound'|translate }}
        {% endif %}
    </div>
{% endif %}

", "@Marketplace/plugin-list.twig", "D:\\xampp\\htdocs\\matomo\\plugins\\Marketplace\\templates\\plugin-list.twig");
    }
}


/* @Marketplace/plugin-list.twig */
class __TwigTemplate_e5c947cb1abc6a6cd58b54d995bae330825dfd504d72c8c7aadd2b1dd9a16756_488967171 extends Twig_Template
{
    public function __construct(Twig_Environment $env)
    {
        parent::__construct($env);

        // line 7
        $this->parent = $this->loadTemplate("contentBlock.twig", "@Marketplace/plugin-list.twig", 7);
        $this->blocks = array(
            'content' => array($this, 'block_content'),
        );
    }

    protected function doGetParent(array $context)
    {
        return "contentBlock.twig";
    }

    protected function doDisplay(array $context, array $blocks = array())
    {
        $this->parent->display($context, array_merge($this->blocks, $blocks));
    }

    // line 8
    public function block_content($context, array $blocks = array())
    {
        // line 9
        echo "                        <div class=\"plugin\">
                            <h3 class=\"card-title\" title=\"";
        // line 10
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_MoreDetails")), "html", null, true);
        echo "\">
                                <a href=\"#\" piwik-plugin-name=\"";
        // line 11
        echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "name", array()), "html", null, true);
        echo "\">";
        echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "displayName", array()), "html", null, true);
        echo "</a>
                            </h3>

                            <p class=\"description\">
                                ";
        // line 15
        echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "description", array()), "html", null, true);
        echo "
                                <a class=\"more\" href=\"#\" piwik-plugin-name=\"";
        // line 16
        echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "name", array()), "html", null, true);
        echo "\" title=\"";
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_MoreDetails")), "html", null, true);
        echo "\">
                                    &rsaquo; ";
        // line 17
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_MoreLowerCase")), "html", null, true);
        echo "</a>
                            </p>

                            ";
        // line 20
        if (($context["showThemes"] ?? $this->getContext($context, "showThemes"))) {
            // line 21
            echo "                                ";
            // line 22
            echo "                                <a class=\"more\" href=\"#\" piwik-plugin-name=\"";
            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "name", array()), "html", null, true);
            echo "\">
                                    <img title=\"";
            // line 23
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_MoreDetails")), "html", null, true);
            echo "\"
                                         class=\"preview\" src=\"";
            // line 24
            echo \Piwik\piwik_escape_filter($this->env, twig_first($this->env, $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "screenshots", array())), "html", null, true);
            echo "?w=250&h=150\"/></a>
                            ";
        }
        // line 26
        echo "
                            <ul class=\"metadata\">
                                ";
        // line 28
        if (( !$this->getAttribute(($context["plugin"] ?? null), "isBundle", array(), "any", true, true) ||  !$this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "isBundle", array()))) {
            // line 29
            echo "                                    <li>
                                        ";
            // line 30
            if ($this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "latestVersion", array())) {
                // line 31
                echo "                                            ";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_Version")), "html", null, true);
                echo ": ";
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "latestVersion", array()), "html", null, true);
                echo "
                                        ";
            }
            // line 33
            echo "
                                        ";
            // line 34
            if ($this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "canBeUpdated", array())) {
                // line 35
                echo "                                            <a class=\"update-available\"
                                                ";
                // line 36
                if (((($this->getAttribute(($context["plugin"] ?? null), "changelog", array(), "any", true, true) && $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "changelog", array())) && $this->getAttribute($this->getAttribute(($context["plugin"] ?? null), "changelog", array(), "any", false, true), "url", array(), "any", true, true)) && $this->getAttribute($this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "changelog", array()), "url", array()))) {
                    // line 37
                    echo "                                                    target=\"_blank\" href=\"";
                    echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "changelog", array()), "url", array()), "html_attr");
                    echo "\"
                                                ";
                } else {
                    // line 39
                    echo "                                                    href=\"#\" piwik-plugin-name=\"";
                    echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "name", array()), "html", null, true);
                    echo "\"
                                                ";
                }
                // line 41
                echo "                                               title=\"";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_PluginUpdateAvailable", $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "currentVersion", array()), $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "latestVersion", array()))), "html", null, true);
                echo "\">
                                                ";
                // line 42
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_NewVersion")), "html", null, true);
                echo "</a>
                                        ";
            }
            // line 44
            echo "                                    </li>
                                    ";
            // line 45
            if ($this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "lastUpdated", array())) {
                // line 46
                echo "                                        <li>";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_Updated")), "html", null, true);
                echo ": ";
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "lastUpdated", array()), "html", null, true);
                echo "</li>
                                    ";
            }
            // line 48
            echo "                                    ";
            if ($this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "numDownloads", array())) {
                // line 49
                echo "                                        <li>";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Downloads")), "html", null, true);
                echo ": ";
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "numDownloads", array()), "html", null, true);
                echo "</li>
                                    ";
            }
            // line 51
            echo "                                    <li>";
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_Developer")), "html", null, true);
            echo ": ";
            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["marketplaceMacro"] ?? $this->getContext($context, "marketplaceMacro")), "pluginDeveloper", array(0 => $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "owner", array())), "method"), "html", null, true);
            echo "</li>
                                ";
        }
        // line 53
        echo "                            </ul>

                            ";
        // line 76
        echo "

                            ";
        // line 78
        if (($context["isSuperUser"] ?? $this->getContext($context, "isSuperUser"))) {
            // line 79
            echo "                                <div class=\"footer\">
                                    ";
            // line 80
            if (($this->getAttribute(($context["plugin"] ?? null), "isMissingLicense", array(), "any", true, true) && $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "isMissingLicense", array()))) {
                // line 81
                echo "
                                        <div class=\"alert alert-danger\" >
                                            ";
                // line 83
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_LicenseMissing")), "html", null, true);
                echo "

                                            <span style=\"white-space:nowrap\">(<a class=\"plugin-details\" href=\"#\" piwik-plugin-name=\"";
                // line 85
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "name", array()), "html", null, true);
                echo "\" title=\"";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_MoreDetails")), "html", null, true);
                echo "\">";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Help")), "html", null, true);
                echo "</a>)</span>
                                        </div>

                                    ";
            } elseif (($this->getAttribute(            // line 88
($context["plugin"] ?? null), "hasExceededLicense", array(), "any", true, true) && $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "hasExceededLicense", array()))) {
                // line 89
                echo "
                                        <div class=\"alert alert-danger\">
                                            ";
                // line 91
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_LicenseExceeded")), "html", null, true);
                echo "

                                            <span style=\"white-space:nowrap\">(<a class=\"plugin-details\" href=\"#\" piwik-plugin-name=\"";
                // line 93
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "name", array()), "html", null, true);
                echo "\" title=\"";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_MoreDetails")), "html", null, true);
                echo "\">";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Help")), "html", null, true);
                echo "</a>)</span>
                                        </div>

                                    ";
            } elseif ((($this->getAttribute(            // line 96
($context["plugin"] ?? $this->getContext($context, "plugin")), "canBeUpdated", array()) && (0 == twig_length_filter($this->env, $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "missingRequirements", array())))) && ($context["isAutoUpdatePossible"] ?? $this->getContext($context, "isAutoUpdatePossible")))) {
                // line 97
                echo "                                        <a class=\"btn btn-block\"
                                           href=\"";
                // line 98
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFunction('linkTo')->getCallable(), array(array("module" => "Marketplace", "action" => "updatePlugin", "pluginName" => $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "name", array()), "nonce" => ($context["updateNonce"] ?? $this->getContext($context, "updateNonce"))))), "html", null, true);
                echo "\">
                                            ";
                // line 99
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CoreUpdater_UpdateTitle")), "html", null, true);
                echo "
                                        </a>
                                    ";
            } elseif (((twig_length_filter($this->env, $this->getAttribute(            // line 101
($context["plugin"] ?? $this->getContext($context, "plugin")), "missingRequirements", array())) > 0) ||  !($context["isAutoUpdatePossible"] ?? $this->getContext($context, "isAutoUpdatePossible")))) {
                // line 102
                echo "
                                        ";
                // line 112
                echo "
                                        ";
                // line 113
                if (($this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "canBeUpdated", array()) && (0 == twig_length_filter($this->env, $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "missingRequirements", array()))))) {
                    // line 114
                    echo "                                            ";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_CannotUpdate")), "html", null, true);
                    echo "
                                            <span style=\"white-space:nowrap\">(<a class=\"plugin-details\" href=\"#\" piwik-plugin-name=\"";
                    // line 115
                    echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "name", array()), "html", null, true);
                    echo "\" title=\"";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_MoreDetails")), "html", null, true);
                    echo "\">";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Help")), "html", null, true);
                    echo "</a>";
                    echo $this->getAttribute($this, "downloadButton", array(0 => true, 1 => ($context["plugin"] ?? $this->getContext($context, "plugin")), 2 => ($context["isAutoUpdatePossible"] ?? $this->getContext($context, "isAutoUpdatePossible"))), "method");
                    echo ")</span>
                                        ";
                } elseif ($this->getAttribute(                // line 116
($context["plugin"] ?? $this->getContext($context, "plugin")), "isInstalled", array())) {
                    // line 117
                    echo "                                            ";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Installed")), "html", null, true);
                    echo "
                                            ";
                    // line 118
                    echo $this->getAttribute($this, "downloadButton", array(0 => false, 1 => ($context["plugin"] ?? $this->getContext($context, "plugin")), 2 => ($context["isAutoUpdatePossible"] ?? $this->getContext($context, "isAutoUpdatePossible")), 3 => true), "method");
                    echo "
                                        ";
                } elseif ( !$this->getAttribute(                // line 119
($context["plugin"] ?? $this->getContext($context, "plugin")), "isDownloadable", array())) {
                    // line 120
                    echo "                                            ";
                    echo $this->getAttribute($this, "moreDetailsLink", array(0 => ($context["plugin"] ?? $this->getContext($context, "plugin"))), "method");
                    echo "
                                        ";
                } else {
                    // line 122
                    echo "                                            ";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_CannotInstall")), "html", null, true);
                    echo "

                                            <span style=\"white-space:nowrap\">(<a class=\"plugin-details\" href=\"#\" piwik-plugin-name=\"";
                    // line 124
                    echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "name", array()), "html", null, true);
                    echo "\" title=\"";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_MoreDetails")), "html", null, true);
                    echo "\">";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Help")), "html", null, true);
                    echo "</a>";
                    echo $this->getAttribute($this, "downloadButton", array(0 => true, 1 => ($context["plugin"] ?? $this->getContext($context, "plugin")), 2 => ($context["isAutoUpdatePossible"] ?? $this->getContext($context, "isAutoUpdatePossible"))), "method");
                    echo ")</span>
                                        ";
                }
                // line 126
                echo "
                                    ";
            } elseif ($this->getAttribute(            // line 127
($context["plugin"] ?? $this->getContext($context, "plugin")), "isInstalled", array())) {
                // line 128
                echo "                                        ";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Installed")), "html", null, true);
                echo "

                                        ";
                // line 130
                if ((( !$this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "isInvalid", array()) &&  !($context["isMultiServerEnvironment"] ?? $this->getContext($context, "isMultiServerEnvironment"))) && ($context["isPluginsAdminEnabled"] ?? $this->getContext($context, "isPluginsAdminEnabled")))) {
                    // line 131
                    echo "                                            (";
                    echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["pluginsMacro"] ?? $this->getContext($context, "pluginsMacro")), "pluginActivateDeactivateAction", array(0 => $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "name", array()), 1 => $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "isActivated", array()), 2 => $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "missingRequirements", array()), 3 => ($context["deactivateNonce"] ?? $this->getContext($context, "deactivateNonce")), 4 => ($context["activateNonce"] ?? $this->getContext($context, "activateNonce"))), "method"), "html", null, true);
                    echo ")
                                        ";
                }
                // line 133
                echo "
                                    ";
            } elseif (($this->getAttribute(            // line 134
($context["plugin"] ?? $this->getContext($context, "plugin")), "isPaid", array()) &&  !$this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "isDownloadable", array()))) {
                // line 135
                echo "                                        ";
                echo $this->getAttribute($this, "moreDetailsLink", array(0 => ($context["plugin"] ?? $this->getContext($context, "plugin"))), "method");
                echo "
                                    ";
            } else {
                // line 137
                echo "                                        <a href=\"";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFunction('linkTo')->getCallable(), array(array("module" => "Marketplace", "action" => "installPlugin", "pluginName" => $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "name", array()), "nonce" => ($context["installNonce"] ?? $this->getContext($context, "installNonce"))))), "html", null, true);
                echo "\"
                                           class=\"btn\">
                                            ";
                // line 139
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_ActionInstall")), "html", null, true);
                echo "
                                        </a>
                                    ";
            }
            // line 142
            echo "                                </div>
                            ";
        } else {
            // line 144
            echo "                                <div class=\"footer\">
                                    ";
            // line 145
            echo $this->getAttribute($this, "moreDetailsLink", array(0 => ($context["plugin"] ?? $this->getContext($context, "plugin"))), "method");
            echo "
                                </div>
                            ";
        }
        // line 148
        echo "
                        </div>
                    ";
    }

    // line 55
    public function getmoreDetailsLink($__plugin__ = null, ...$__varargs__)
    {
        $context = $this->env->mergeGlobals(array(
            "plugin" => $__plugin__,
            "varargs" => $__varargs__,
        ));

        $blocks = array();

        ob_start();
        try {
            // line 56
            echo "                                ";
            $context["canBePurchased"] = ((( !$this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "isDownloadable", array()) && $this->getAttribute(($context["plugin"] ?? null), "shop", array(), "any", true, true)) && $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "shop", array())) && $this->getAttribute($this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "shop", array()), "url", array()));
            // line 57
            echo "                                <a class=\"btn btn-block plugin-details ";
            if (($context["canBePurchased"] ?? $this->getContext($context, "canBePurchased"))) {
                echo "purchaseable";
            }
            echo "\" href=\"#\" piwik-plugin-name=\"";
            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "name", array()), "html", null, true);
            echo "\" title=\"";
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_MoreDetails")), "html", null, true);
            echo "\">

                                    ";
            // line 59
            if ((($context["canBePurchased"] ?? $this->getContext($context, "canBePurchased")) && $this->getAttribute($this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "shop", array()), "variations", array()))) {
                // line 60
                echo "                                        ";
                $context["foundCheapest"] = 0;
                // line 61
                echo "                                        ";
                $context['_parent'] = $context;
                $context['_seq'] = twig_ensure_traversable($this->getAttribute($this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "shop", array()), "variations", array()));
                foreach ($context['_seq'] as $context["_key"] => $context["variation"]) {
                    // line 62
                    echo "                                            ";
                    if ((( !($context["foundCheapest"] ?? $this->getContext($context, "foundCheapest")) && $this->getAttribute($context["variation"], "cheapest", array(), "any", true, true)) && $this->getAttribute($context["variation"], "cheapest", array()))) {
                        // line 63
                        echo "                                                ";
                        $context["foundCheapest"] = 1;
                        // line 64
                        echo "                                                ";
                        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_PriceFromPerPeriod", $this->getAttribute($context["variation"], "prettyPrice", array()), $this->getAttribute($context["variation"], "period", array()))), "html", null, true);
                        echo "
                                            ";
                    }
                    // line 66
                    echo "                                        ";
                }
                $_parent = $context['_parent'];
                unset($context['_seq'], $context['_iterated'], $context['_key'], $context['variation'], $context['_parent'], $context['loop']);
                $context = array_intersect_key($context, $_parent) + $_parent;
                // line 67
                echo "                                        ";
                if ( !($context["foundCheapest"] ?? $this->getContext($context, "foundCheapest"))) {
                    // line 68
                    echo "                                            ";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_PriceFromPerPeriod", $this->getAttribute($this->getAttribute($this->getAttribute($this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "shop", array()), "variations", array()), 0, array()), "prettyPrice", array()), $this->getAttribute($this->getAttribute($this->getAttribute($this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "shop", array()), "variations", array()), 0, array()), "period", array()))), "html", null, true);
                    echo "
                                        ";
                }
                // line 70
                echo "                                    ";
            } else {
                // line 71
                echo "                                        ";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_MoreDetails")), "html", null, true);
                echo "
                                    ";
            }
            // line 73
            echo "
                                </a>
                            ";
        } catch (Exception $e) {
            ob_end_clean();

            throw $e;
        } catch (Throwable $e) {
            ob_end_clean();

            throw $e;
        }

        return ('' === $tmp = ob_get_clean()) ? '' : new Twig_Markup($tmp, $this->env->getCharset());
    }

    // line 103
    public function getdownloadButton($__showOr__ = null, $__plugin__ = null, $__isAutoUpdatePossible__ = null, $__showBrackets__ = false, ...$__varargs__)
    {
        $context = $this->env->mergeGlobals(array(
            "showOr" => $__showOr__,
            "plugin" => $__plugin__,
            "isAutoUpdatePossible" => $__isAutoUpdatePossible__,
            "showBrackets" => $__showBrackets__,
            "varargs" => $__varargs__,
        ));

        $blocks = array();

        ob_start();
        try {
            // line 104
            if ((((twig_length_filter($this->env, $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "missingRequirements", array())) == 0) && $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "isDownloadable", array())) &&  !($context["isAutoUpdatePossible"] ?? $this->getContext($context, "isAutoUpdatePossible")))) {
                // line 105
                if (($context["showBrackets"] ?? $this->getContext($context, "showBrackets"))) {
                    echo "(";
                }
                echo "<span onclick=\"\$(this).css('display', 'none')\">";
                // line 106
                if (($context["showOr"] ?? $this->getContext($context, "showOr"))) {
                    echo " ";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Or")), "html", null, true);
                    echo " ";
                }
                // line 107
                echo "<a class=\"plugin-details download\"
                                                   href=\"";
                // line 108
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFunction('linkTo')->getCallable(), array(array("module" => "Marketplace", "action" => "download", "pluginName" => $this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "name", array()), "nonce" => Piwik\Nonce::getNonce($this->getAttribute(($context["plugin"] ?? $this->getContext($context, "plugin")), "name", array()))))), "html", null, true);
                echo "\"
                                                >";
                // line 109
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Download")), "html", null, true);
                echo "</a></span>";
                if (($context["showBrackets"] ?? $this->getContext($context, "showBrackets"))) {
                    echo ")";
                }
            }
        } catch (Exception $e) {
            ob_end_clean();

            throw $e;
        } catch (Throwable $e) {
            ob_end_clean();

            throw $e;
        }

        return ('' === $tmp = ob_get_clean()) ? '' : new Twig_Markup($tmp, $this->env->getCharset());
    }

    public function getTemplateName()
    {
        return "@Marketplace/plugin-list.twig";
    }

    public function isTraitable()
    {
        return false;
    }

    public function getDebugInfo()
    {
        return array (  773 => 109,  769 => 108,  766 => 107,  760 => 106,  755 => 105,  753 => 104,  738 => 103,  721 => 73,  715 => 71,  712 => 70,  706 => 68,  703 => 67,  697 => 66,  691 => 64,  688 => 63,  685 => 62,  680 => 61,  677 => 60,  675 => 59,  663 => 57,  660 => 56,  648 => 55,  642 => 148,  636 => 145,  633 => 144,  629 => 142,  623 => 139,  617 => 137,  611 => 135,  609 => 134,  606 => 133,  600 => 131,  598 => 130,  592 => 128,  590 => 127,  587 => 126,  576 => 124,  570 => 122,  564 => 120,  562 => 119,  558 => 118,  553 => 117,  551 => 116,  541 => 115,  536 => 114,  534 => 113,  531 => 112,  528 => 102,  526 => 101,  521 => 99,  517 => 98,  514 => 97,  512 => 96,  502 => 93,  497 => 91,  493 => 89,  491 => 88,  481 => 85,  476 => 83,  472 => 81,  470 => 80,  467 => 79,  465 => 78,  461 => 76,  457 => 53,  449 => 51,  441 => 49,  438 => 48,  430 => 46,  428 => 45,  425 => 44,  420 => 42,  415 => 41,  409 => 39,  403 => 37,  401 => 36,  398 => 35,  396 => 34,  393 => 33,  385 => 31,  383 => 30,  380 => 29,  378 => 28,  374 => 26,  369 => 24,  365 => 23,  360 => 22,  358 => 21,  356 => 20,  350 => 17,  344 => 16,  340 => 15,  331 => 11,  327 => 10,  324 => 9,  321 => 8,  304 => 7,  96 => 166,  92 => 164,  86 => 162,  80 => 160,  78 => 159,  75 => 158,  73 => 157,  70 => 156,  66 => 154,  51 => 152,  49 => 7,  46 => 6,  29 => 5,  26 => 4,  24 => 3,  21 => 2,  19 => 1,);
    }

    /** @deprecated since 1.27 (to be removed in 2.0). Use getSourceContext() instead */
    public function getSource()
    {
        @trigger_error('The '.__METHOD__.' method is deprecated since version 1.27 and will be removed in 2.0. Use getSourceContext() instead.', E_USER_DEPRECATED);

        return $this->getSourceContext()->getCode();
    }

    public function getSourceContext()
    {
        return new Twig_Source("{% import '@Marketplace/macros.twig' as marketplaceMacro %}

{% if pluginsToShow|length > 0 %}
    <div class=\"pluginListContainer row\">
        {% for plugin in pluginsToShow %}
            <div class=\"col s12 m6 l4\">
                {% embed 'contentBlock.twig' with {'title': ''} %}
                    {% block content %}
                        <div class=\"plugin\">
                            <h3 class=\"card-title\" title=\"{{ 'General_MoreDetails'|translate }}\">
                                <a href=\"#\" piwik-plugin-name=\"{{ plugin.name }}\">{{ plugin.displayName }}</a>
                            </h3>

                            <p class=\"description\">
                                {{ plugin.description }}
                                <a class=\"more\" href=\"#\" piwik-plugin-name=\"{{ plugin.name }}\" title=\"{{ 'General_MoreDetails'|translate }}\">
                                    &rsaquo; {{ 'General_MoreLowerCase'|translate }}</a>
                            </p>

                            {% if showThemes %}
                                {# Screenshot for themes #}
                                <a class=\"more\" href=\"#\" piwik-plugin-name=\"{{ plugin.name }}\">
                                    <img title=\"{{ 'General_MoreDetails'|translate }}\"
                                         class=\"preview\" src=\"{{ plugin.screenshots|first }}?w=250&h=150\"/></a>
                            {% endif %}

                            <ul class=\"metadata\">
                                {% if plugin.isBundle is not defined or not plugin.isBundle %}
                                    <li>
                                        {% if plugin.latestVersion %}
                                            {{ 'CorePluginsAdmin_Version'|translate }}: {{ plugin.latestVersion }}
                                        {% endif %}

                                        {% if plugin.canBeUpdated %}
                                            <a class=\"update-available\"
                                                {% if plugin.changelog is defined and plugin.changelog and plugin.changelog.url is defined and plugin.changelog.url %}
                                                    target=\"_blank\" href=\"{{ plugin.changelog.url|e('html_attr') }}\"
                                                {% else %}
                                                    href=\"#\" piwik-plugin-name=\"{{ plugin.name }}\"
                                                {% endif %}
                                               title=\"{{ 'Marketplace_PluginUpdateAvailable'|translate(plugin.currentVersion, plugin.latestVersion) }}\">
                                                {{ 'Marketplace_NewVersion'|translate }}</a>
                                        {% endif %}
                                    </li>
                                    {% if plugin.lastUpdated %}
                                        <li>{{ 'Marketplace_Updated'|translate }}: {{ plugin.lastUpdated }}</li>
                                    {% endif %}
                                    {% if plugin.numDownloads %}
                                        <li>{{ 'General_Downloads'|translate }}: {{ plugin.numDownloads }}</li>
                                    {% endif %}
                                    <li>{{ 'Marketplace_Developer'|translate }}: {{ marketplaceMacro.pluginDeveloper(plugin.owner) }}</li>
                                {% endif %}
                            </ul>

                            {% macro moreDetailsLink(plugin) %}
                                {% set canBePurchased = not plugin.isDownloadable and plugin.shop is defined and plugin.shop and plugin.shop.url %}
                                <a class=\"btn btn-block plugin-details {% if canBePurchased %}purchaseable{% endif %}\" href=\"#\" piwik-plugin-name=\"{{ plugin.name }}\" title=\"{{ 'General_MoreDetails'|translate }}\">

                                    {% if canBePurchased and plugin.shop.variations %}
                                        {% set foundCheapest = 0 %}
                                        {% for variation in plugin.shop.variations %}
                                            {% if not foundCheapest and variation.cheapest is defined and variation.cheapest %}
                                                {% set foundCheapest = 1 %}
                                                {{ 'Marketplace_PriceFromPerPeriod'|translate(variation.prettyPrice, variation.period) }}
                                            {% endif %}
                                        {% endfor %}
                                        {% if not foundCheapest %}
                                            {{ 'Marketplace_PriceFromPerPeriod'|translate(plugin.shop.variations.0.prettyPrice, plugin.shop.variations.0.period) }}
                                        {% endif %}
                                    {% else %}
                                        {{ 'General_MoreDetails'|translate }}
                                    {% endif %}

                                </a>
                            {% endmacro %}


                            {% if isSuperUser %}
                                <div class=\"footer\">
                                    {% if plugin.isMissingLicense is defined and plugin.isMissingLicense %}

                                        <div class=\"alert alert-danger\" >
                                            {{ 'Marketplace_LicenseMissing'|translate }}

                                            <span style=\"white-space:nowrap\">(<a class=\"plugin-details\" href=\"#\" piwik-plugin-name=\"{{ plugin.name }}\" title=\"{{ 'General_MoreDetails'|translate }}\">{{ 'General_Help'|translate }}</a>)</span>
                                        </div>

                                    {% elseif plugin.hasExceededLicense is defined and plugin.hasExceededLicense %}

                                        <div class=\"alert alert-danger\">
                                            {{ 'Marketplace_LicenseExceeded'|translate }}

                                            <span style=\"white-space:nowrap\">(<a class=\"plugin-details\" href=\"#\" piwik-plugin-name=\"{{ plugin.name }}\" title=\"{{ 'General_MoreDetails'|translate }}\">{{ 'General_Help'|translate }}</a>)</span>
                                        </div>

                                    {% elseif plugin.canBeUpdated and 0 == plugin.missingRequirements|length and isAutoUpdatePossible %}
                                        <a class=\"btn btn-block\"
                                           href=\"{{ linkTo({'module': 'Marketplace', 'action':'updatePlugin', 'pluginName': plugin.name, 'nonce': updateNonce}) }}\">
                                            {{ 'CoreUpdater_UpdateTitle'|translate }}
                                        </a>
                                    {% elseif plugin.missingRequirements|length > 0 or not isAutoUpdatePossible %}

                                        {% macro downloadButton(showOr, plugin, isAutoUpdatePossible, showBrackets = false) -%}
                                            {%- if plugin.missingRequirements|length == 0 and plugin.isDownloadable and not isAutoUpdatePossible -%}
                                                {% if showBrackets %}({% endif %}<span onclick=\"\$(this).css('display', 'none')\">
                                                {%- if showOr %} {{ 'General_Or'|translate }} {% endif -%}
                                                <a class=\"plugin-details download\"
                                                   href=\"{{ linkTo({'module': 'Marketplace', 'action': 'download', 'pluginName': plugin.name, 'nonce': (plugin.name|nonce)}) }}\"
                                                >{{ 'General_Download'|translate }}</a></span>{% if showBrackets %}){% endif %}
                                            {%- endif -%}
                                        {%- endmacro %}

                                        {% if plugin.canBeUpdated and 0 == plugin.missingRequirements|length %}
                                            {{ 'Marketplace_CannotUpdate'|translate }}
                                            <span style=\"white-space:nowrap\">(<a class=\"plugin-details\" href=\"#\" piwik-plugin-name=\"{{ plugin.name }}\" title=\"{{ 'General_MoreDetails'|translate }}\">{{ 'General_Help'|translate }}</a>{{ _self.downloadButton(true, plugin, isAutoUpdatePossible)|raw }})</span>
                                        {% elseif plugin.isInstalled %}
                                            {{ 'General_Installed'|translate }}
                                            {{ _self.downloadButton(false, plugin, isAutoUpdatePossible, true)|raw }}
                                        {% elseif not plugin.isDownloadable %}
                                            {{ _self.moreDetailsLink(plugin)|raw }}
                                        {% else %}
                                            {{ 'Marketplace_CannotInstall'|translate }}

                                            <span style=\"white-space:nowrap\">(<a class=\"plugin-details\" href=\"#\" piwik-plugin-name=\"{{ plugin.name }}\" title=\"{{ 'General_MoreDetails'|translate }}\">{{ 'General_Help'|translate }}</a>{{ _self.downloadButton(true, plugin, isAutoUpdatePossible)|raw }})</span>
                                        {% endif %}

                                    {% elseif plugin.isInstalled %}
                                        {{ 'General_Installed'|translate }}

                                        {% if not plugin.isInvalid and not isMultiServerEnvironment and isPluginsAdminEnabled %}
                                            ({{ pluginsMacro.pluginActivateDeactivateAction(plugin.name, plugin.isActivated, plugin.missingRequirements, deactivateNonce, activateNonce) }})
                                        {% endif %}

                                    {% elseif plugin.isPaid and not plugin.isDownloadable %}
                                        {{ _self.moreDetailsLink(plugin)|raw }}
                                    {% else %}
                                        <a href=\"{{ linkTo({'module': 'Marketplace', 'action': 'installPlugin', 'pluginName': plugin.name, 'nonce': installNonce}) }}\"
                                           class=\"btn\">
                                            {{ 'Marketplace_ActionInstall'|translate }}
                                        </a>
                                    {% endif %}
                                </div>
                            {% else %}
                                <div class=\"footer\">
                                    {{ _self.moreDetailsLink(plugin)|raw }}
                                </div>
                            {% endif %}

                        </div>
                    {% endblock %}
                {% endembed %}
            </div>
        {% endfor %}
    </div>
{% endif %}

{% if pluginsToShow|length == 0 %}
    <div piwik-content-block>
        {% if showThemes %}
            {{ 'Marketplace_NoThemesFound'|translate }}
        {% else %}
            {{ 'Marketplace_NoPluginsFound'|translate }}
        {% endif %}
    </div>
{% endif %}

", "@Marketplace/plugin-list.twig", "D:\\xampp\\htdocs\\matomo\\plugins\\Marketplace\\templates\\plugin-list.twig");
    }
}
