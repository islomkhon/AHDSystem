<?php

/* @CoreHome/_menu.twig */
class __TwigTemplate_f56f180b20f92d94bd903c74ebb9c50baaed3480a517e57fff85bb4606c5dcc5 extends Twig_Template
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
    }

    // line 1
    public function getmenu($__menu__ = null, $__anchorlink__ = null, $__cssClass__ = null, $__currentModule__ = null, $__currentAction__ = null, ...$__varargs__)
    {
        $context = $this->env->mergeGlobals(array(
            "menu" => $__menu__,
            "anchorlink" => $__anchorlink__,
            "cssClass" => $__cssClass__,
            "currentModule" => $__currentModule__,
            "currentAction" => $__currentAction__,
            "varargs" => $__varargs__,
        ));

        $blocks = array();

        ob_start();
        try {
            // line 2
            echo "    <div id=\"secondNavBar\" class=\"";
            echo \Piwik\piwik_escape_filter($this->env, ($context["cssClass"] ?? $this->getContext($context, "cssClass")), "html", null, true);
            echo " z-depth-1\">
        <ul class=\"navbar hide-on-med-and-down\" aria-label=\"";
            // line 3
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CoreHome_MainNavigation")), "html_attr");
            echo "\" role=\"menu\">
            ";
            // line 4
            $context['_parent'] = $context;
            $context['_seq'] = twig_ensure_traversable(($context["menu"] ?? $this->getContext($context, "menu")));
            foreach ($context['_seq'] as $context["level1"] => $context["level2"]) {
                // line 5
                echo "
                ";
                // line 6
                $context["hasSubmenuItem"] = false;
                // line 7
                echo "                ";
                $context['_parent'] = $context;
                $context['_seq'] = twig_ensure_traversable($context["level2"]);
                foreach ($context['_seq'] as $context["name"] => $context["urlParameters"]) {
                    // line 8
                    echo "                    ";
                    if ((twig_slice($this->env, $context["name"], 0, 1) != "_")) {
                        // line 9
                        echo "                        ";
                        $context["hasSubmenuItem"] = true;
                        // line 10
                        echo "                    ";
                    }
                    // line 11
                    echo "                ";
                }
                $_parent = $context['_parent'];
                unset($context['_seq'], $context['_iterated'], $context['name'], $context['urlParameters'], $context['_parent'], $context['loop']);
                $context = array_intersect_key($context, $_parent) + $_parent;
                // line 12
                echo "
                ";
                // line 13
                if (($context["hasSubmenuItem"] ?? $this->getContext($context, "hasSubmenuItem"))) {
                    // line 14
                    echo "                    <li class=\"menuTab\" role=\"menuitem\">

                        <a class=\"item\" tabindex=\"5\">
                            <span class=\"menu-icon ";
                    // line 17
                    echo \Piwik\piwik_escape_filter($this->env, (($this->getAttribute($context["level2"], "_icon", array(), "any", true, true)) ? (_twig_default_filter($this->getAttribute($context["level2"], "_icon", array()), "icon-arrow-right")) : ("icon-arrow-right")), "html", null, true);
                    echo "\"></span>";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array($context["level1"])), "html", null, true);
                    echo "
                            <span class=\"hidden\">
                             ";
                    // line 19
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CoreHome_Menu")), "html", null, true);
                    echo "
                           </span>
                        </a>
                        <ul role=\"menu\">
                            ";
                    // line 23
                    $context['_parent'] = $context;
                    $context['_seq'] = twig_ensure_traversable($context["level2"]);
                    foreach ($context['_seq'] as $context["name"] => $context["urlParameters"]) {
                        // line 24
                        echo "                                ";
                        if ((twig_slice($this->env, $context["name"], 0, 1) != "_")) {
                            // line 25
                            echo "                                    <li ";
                            if (((($this->getAttribute($this->getAttribute($context["urlParameters"], "_url", array(), "any", false, true), "module", array(), "any", true, true) && ($this->getAttribute($this->getAttribute($context["urlParameters"], "_url", array()), "module", array()) == ($context["currentModule"] ?? $this->getContext($context, "currentModule")))) && $this->getAttribute($this->getAttribute($context["urlParameters"], "_url", array(), "any", false, true), "action", array(), "any", true, true)) && ($this->getAttribute($this->getAttribute($context["urlParameters"], "_url", array()), "action", array()) == ($context["currentAction"] ?? $this->getContext($context, "currentAction"))))) {
                                echo "class=\"active\"";
                            }
                            // line 26
                            echo "                                        role=\"menuitem\"
                                    >
                                        <a class=\"item\" tabindex=\"5\" target=\"_self\"
                                           title=\"";
                            // line 29
                            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array((($this->getAttribute($context["urlParameters"], "_tooltip", array(), "any", true, true)) ? (_twig_default_filter($this->getAttribute($context["urlParameters"], "_tooltip", array()), "")) : ("")))), "html_attr");
                            echo "\"
                                            ";
                            // line 30
                            if (($this->getAttribute($context["urlParameters"], "_onclick", array(), "any", true, true) && $this->getAttribute($context["urlParameters"], "_onclick", array()))) {
                                // line 31
                                echo "                                                onclick=\"";
                                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["urlParameters"], "_onclick", array()), "html_attr");
                                echo ";return false;\"
                                            ";
                            }
                            // line 33
                            echo "                                            ";
                            if ($this->getAttribute($context["urlParameters"], "_url", array())) {
                                // line 34
                                echo "                                                href=\"index.php?";
                                echo \Piwik\piwik_escape_filter($this->env, twig_slice($this->env, call_user_func_array($this->env->getFilter('urlRewriteWithParameters')->getCallable(), array($this->getAttribute($context["urlParameters"], "_url", array()))), 1), "html", null, true);
                                echo "\"
                                            ";
                            }
                            // line 35
                            echo ">
                                            ";
                            // line 36
                            if (($this->getAttribute($context["urlParameters"], "_icon", array(), "any", true, true) && $this->getAttribute($context["urlParameters"], "_icon", array()))) {
                                echo "<span class=\"icon ";
                                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["urlParameters"], "_icon", array()), "html_attr");
                                echo "\" style=\"margin-right: 5px;\"></span>";
                            }
                            // line 37
                            echo "                                            ";
                            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array($context["name"])), "html", null, true);
                            echo "
                                        </a>
                                    </li>
                                ";
                        }
                        // line 41
                        echo "                            ";
                    }
                    $_parent = $context['_parent'];
                    unset($context['_seq'], $context['_iterated'], $context['name'], $context['urlParameters'], $context['_parent'], $context['loop']);
                    $context = array_intersect_key($context, $_parent) + $_parent;
                    // line 42
                    echo "                        </ul>
                    </li>
                ";
                }
                // line 45
                echo "            ";
            }
            $_parent = $context['_parent'];
            unset($context['_seq'], $context['_iterated'], $context['level1'], $context['level2'], $context['_parent'], $context['loop']);
            $context = array_intersect_key($context, $_parent) + $_parent;
            // line 46
            echo "        </ul>
        <ul id=\"mobile-left-menu\" class=\"side-nav hide-on-large-only\">
            ";
            // line 48
            $context['_parent'] = $context;
            $context['_seq'] = twig_ensure_traversable(($context["menu"] ?? $this->getContext($context, "menu")));
            foreach ($context['_seq'] as $context["level1"] => $context["level2"]) {
                // line 49
                echo "
            ";
                // line 50
                $context["hasSubmenuItem"] = false;
                // line 51
                echo "            ";
                $context['_parent'] = $context;
                $context['_seq'] = twig_ensure_traversable($context["level2"]);
                foreach ($context['_seq'] as $context["name"] => $context["urlParameters"]) {
                    // line 52
                    echo "                ";
                    if ((twig_slice($this->env, $context["name"], 0, 1) != "_")) {
                        // line 53
                        echo "                    ";
                        $context["hasSubmenuItem"] = true;
                        // line 54
                        echo "                ";
                    }
                    // line 55
                    echo "            ";
                }
                $_parent = $context['_parent'];
                unset($context['_seq'], $context['_iterated'], $context['name'], $context['urlParameters'], $context['_parent'], $context['loop']);
                $context = array_intersect_key($context, $_parent) + $_parent;
                // line 56
                echo "
            ";
                // line 57
                if (($context["hasSubmenuItem"] ?? $this->getContext($context, "hasSubmenuItem"))) {
                    // line 58
                    echo "            <li class=\"no-padding\">
                <ul class=\"collapsible collapsible-accordion\" piwik-side-nav=\"nav .activateLeftMenu\">
                    <li>
                        <a class=\"collapsible-header\">";
                    // line 61
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array($context["level1"])), "html", null, true);
                    echo "<i class=\"";
                    echo \Piwik\piwik_escape_filter($this->env, (($this->getAttribute($context["level2"], "_icon", array(), "any", true, true)) ? (_twig_default_filter($this->getAttribute($context["level2"], "_icon", array()), "icon-arrow-down")) : ("icon-arrow-down")), "html", null, true);
                    echo "\"></i></a>
                        <div class=\"collapsible-body\">
                            <ul>
                                ";
                    // line 64
                    $context['_parent'] = $context;
                    $context['_seq'] = twig_ensure_traversable($context["level2"]);
                    foreach ($context['_seq'] as $context["name"] => $context["urlParameters"]) {
                        // line 65
                        echo "                                    ";
                        if ((twig_slice($this->env, $context["name"], 0, 1) != "_")) {
                            // line 66
                            echo "                                        <li>
                                            <a title=\"";
                            // line 67
                            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array((($this->getAttribute($context["urlParameters"], "_tooltip", array(), "any", true, true)) ? (_twig_default_filter($this->getAttribute($context["urlParameters"], "_tooltip", array()), "")) : ("")))), "html_attr");
                            echo "\" target=\"_self\"
                                               href=\"index.php?";
                            // line 68
                            echo \Piwik\piwik_escape_filter($this->env, twig_slice($this->env, call_user_func_array($this->env->getFilter('urlRewriteWithParameters')->getCallable(), array($this->getAttribute($context["urlParameters"], "_url", array()))), 1), "html", null, true);
                            echo "\">";
                            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array($context["name"])), "html", null, true);
                            echo "</a>
                                        </li>
                                    ";
                        }
                        // line 71
                        echo "                                ";
                    }
                    $_parent = $context['_parent'];
                    unset($context['_seq'], $context['_iterated'], $context['name'], $context['urlParameters'], $context['_parent'], $context['loop']);
                    $context = array_intersect_key($context, $_parent) + $_parent;
                    // line 72
                    echo "                            </ul>
                        </div>
                    </li>
                </ul>
                ";
                }
                // line 77
                echo "                ";
            }
            $_parent = $context['_parent'];
            unset($context['_seq'], $context['_iterated'], $context['level1'], $context['level2'], $context['_parent'], $context['loop']);
            $context = array_intersect_key($context, $_parent) + $_parent;
            // line 78
            echo "            </li>
        </ul>
    </div>
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

    public function getTemplateName()
    {
        return "@CoreHome/_menu.twig";
    }

    public function isTraitable()
    {
        return false;
    }

    public function getDebugInfo()
    {
        return array (  263 => 78,  257 => 77,  250 => 72,  244 => 71,  236 => 68,  232 => 67,  229 => 66,  226 => 65,  222 => 64,  214 => 61,  209 => 58,  207 => 57,  204 => 56,  198 => 55,  195 => 54,  192 => 53,  189 => 52,  184 => 51,  182 => 50,  179 => 49,  175 => 48,  171 => 46,  165 => 45,  160 => 42,  154 => 41,  146 => 37,  140 => 36,  137 => 35,  131 => 34,  128 => 33,  122 => 31,  120 => 30,  116 => 29,  111 => 26,  106 => 25,  103 => 24,  99 => 23,  92 => 19,  85 => 17,  80 => 14,  78 => 13,  75 => 12,  69 => 11,  66 => 10,  63 => 9,  60 => 8,  55 => 7,  53 => 6,  50 => 5,  46 => 4,  42 => 3,  37 => 2,  21 => 1,);
    }

    /** @deprecated since 1.27 (to be removed in 2.0). Use getSourceContext() instead */
    public function getSource()
    {
        @trigger_error('The '.__METHOD__.' method is deprecated since version 1.27 and will be removed in 2.0. Use getSourceContext() instead.', E_USER_DEPRECATED);

        return $this->getSourceContext()->getCode();
    }

    public function getSourceContext()
    {
        return new Twig_Source("{% macro menu(menu, anchorlink, cssClass, currentModule, currentAction) %}
    <div id=\"secondNavBar\" class=\"{{ cssClass }} z-depth-1\">
        <ul class=\"navbar hide-on-med-and-down\" aria-label=\"{{ 'CoreHome_MainNavigation'|translate|e('html_attr') }}\" role=\"menu\">
            {% for level1,level2 in menu %}

                {% set hasSubmenuItem = false %}
                {% for name,urlParameters in level2 %}
                    {% if name|slice(0,1) != '_' %}
                        {% set hasSubmenuItem = true %}
                    {% endif %}
                {% endfor %}

                {% if hasSubmenuItem %}
                    <li class=\"menuTab\" role=\"menuitem\">

                        <a class=\"item\" tabindex=\"5\">
                            <span class=\"menu-icon {{ level2._icon|default('icon-arrow-right') }}\"></span>{{ level1|translate }}
                            <span class=\"hidden\">
                             {{ 'CoreHome_Menu'|translate }}
                           </span>
                        </a>
                        <ul role=\"menu\">
                            {% for name,urlParameters in level2 %}
                                {% if name|slice(0,1) != '_' %}
                                    <li {% if urlParameters._url.module is defined and urlParameters._url.module == currentModule and urlParameters._url.action is defined and urlParameters._url.action == currentAction %}class=\"active\"{% endif %}
                                        role=\"menuitem\"
                                    >
                                        <a class=\"item\" tabindex=\"5\" target=\"_self\"
                                           title=\"{{ urlParameters._tooltip|default('')|translate|e('html_attr') }}\"
                                            {% if urlParameters._onclick is defined and urlParameters._onclick %}
                                                onclick=\"{{ urlParameters._onclick|e('html_attr') }};return false;\"
                                            {% endif %}
                                            {% if urlParameters._url %}
                                                href=\"index.php?{{ urlParameters._url|urlRewriteWithParameters|slice(1) }}\"
                                            {% endif %}>
                                            {% if urlParameters._icon is defined and urlParameters._icon %}<span class=\"icon {{ urlParameters._icon|e('html_attr') }}\" style=\"margin-right: 5px;\"></span>{% endif %}
                                            {{ name|translate }}
                                        </a>
                                    </li>
                                {% endif %}
                            {% endfor %}
                        </ul>
                    </li>
                {% endif %}
            {% endfor %}
        </ul>
        <ul id=\"mobile-left-menu\" class=\"side-nav hide-on-large-only\">
            {% for level1,level2 in menu %}

            {% set hasSubmenuItem = false %}
            {% for name,urlParameters in level2 %}
                {% if name|slice(0,1) != '_' %}
                    {% set hasSubmenuItem = true %}
                {% endif %}
            {% endfor %}

            {% if hasSubmenuItem %}
            <li class=\"no-padding\">
                <ul class=\"collapsible collapsible-accordion\" piwik-side-nav=\"nav .activateLeftMenu\">
                    <li>
                        <a class=\"collapsible-header\">{{ level1|translate }}<i class=\"{{ level2._icon|default('icon-arrow-down') }}\"></i></a>
                        <div class=\"collapsible-body\">
                            <ul>
                                {% for name,urlParameters in level2 %}
                                    {% if name|slice(0,1) != '_' %}
                                        <li>
                                            <a title=\"{{ urlParameters._tooltip|default('')|translate|e('html_attr') }}\" target=\"_self\"
                                               href=\"index.php?{{ urlParameters._url|urlRewriteWithParameters|slice(1) }}\">{{ name|translate }}</a>
                                        </li>
                                    {% endif %}
                                {% endfor %}
                            </ul>
                        </div>
                    </li>
                </ul>
                {% endif %}
                {% endfor %}
            </li>
        </ul>
    </div>
{% endmacro %}
", "@CoreHome/_menu.twig", "D:\\xampp\\htdocs\\matomo\\plugins\\CoreHome\\templates\\_menu.twig");
    }
}
