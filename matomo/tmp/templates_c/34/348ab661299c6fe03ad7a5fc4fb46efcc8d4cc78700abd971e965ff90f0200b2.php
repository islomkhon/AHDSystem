<?php

/* @Live/getLastVisitsStart.twig */
class __TwigTemplate_20a4f8a9fe68134eed21b66d06a85f16a3d300ee73f5f4a7971af5c343c6d8bc extends Twig_Template
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
        // line 2
        $context["maxPagesDisplayedByVisitor"] = 100;
        // line 3
        echo "
<ul id=\"visitsLive\">
    ";
        // line 5
        $context['_parent'] = $context;
        $context['_seq'] = twig_ensure_traversable($this->getAttribute(($context["visitors"] ?? $this->getContext($context, "visitors")), "getRows", array(), "method"));
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
        foreach ($context['_seq'] as $context["_key"] => $context["visitor"]) {
            // line 6
            echo "        <li id=\"";
            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["visitor"], "getColumn", array(0 => "idVisit"), "method"), "html", null, true);
            echo "\" class=\"visit\">
            <div style=\"display:none;\" class=\"idvisit\">";
            // line 7
            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["visitor"], "idVisit", array()), "html", null, true);
            echo "</div>
            <div title=\"";
            // line 8
            echo \Piwik\piwik_escape_filter($this->env, twig_length_filter($this->env, $this->getAttribute($context["visitor"], "getColumn", array(0 => "actionDetails"), "method")), "html", null, true);
            echo " ";
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Actions")), "html", null, true);
            echo "\" class=\"datetime\">
                <span style=\"display:none;\" class=\"serverTimestamp\">";
            // line 9
            echo $this->getAttribute($context["visitor"], "getColumn", array(0 => "serverTimestamp"), "method");
            echo "</span>
                ";
            // line 10
            echo call_user_func_array($this->env->getFunction('postEvent')->getCallable(), array("Live.visitorLogWidgetViewBeforeVisitInfo", $context["visitor"]));
            echo "
                ";
            // line 11
            $context["year"] = twig_date_format_filter($this->env, $this->getAttribute($context["visitor"], "getColumn", array(0 => "serverTimestamp"), "method"), "Y");
            // line 12
            echo "                <span class=\"realTimeWidget_datetime\">";
            echo \Piwik\piwik_escape_filter($this->env, twig_replace_filter($this->getAttribute($context["visitor"], "getColumn", array(0 => "serverDatePretty"), "method"), array(($context["year"] ?? $this->getContext($context, "year")) => " ")), "html", null, true);
            echo " - ";
            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["visitor"], "getColumn", array(0 => "serverTimePretty"), "method"), "html", null, true);
            echo " ";
            if (($this->getAttribute($context["visitor"], "getColumn", array(0 => "visitDuration"), "method") > 0)) {
                echo "(";
                echo $this->getAttribute($context["visitor"], "getColumn", array(0 => "visitDurationPretty"), "method");
                echo ")";
            }
            echo "</span>
                ";
            // line 13
            if ( !twig_test_empty((($this->getAttribute($context["visitor"], "getColumn", array(0 => "userId"), "method", true, true)) ? (_twig_default_filter($this->getAttribute($context["visitor"], "getColumn", array(0 => "userId"), "method"), false)) : (false)))) {
                // line 14
                echo "                  &nbsp;  <a class=\"visits-live-launch-visitor-profile rightLink\" title=\"";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Live_ViewVisitorProfile")), "html", null, true);
                echo " ";
                if ( !twig_test_empty($this->getAttribute($context["visitor"], "getColumn", array(0 => "userId"), "method"))) {
                    echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["visitor"], "getColumn", array(0 => "userId"), "method"), "html", null, true);
                }
                echo "\" data-visitor-id=\"";
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["visitor"], "getColumn", array(0 => "visitorId"), "method"), "html", null, true);
                echo "\">
                        <span>";
                // line 15
                echo call_user_func_array($this->env->getFilter('rawSafeDecoded')->getCallable(), array($this->getAttribute($context["visitor"], "getColumn", array(0 => "userId"), "method")));
                echo "</span>
                    </a>
                ";
            }
            // line 18
            echo "
                ";
            // line 19
            echo call_user_func_array($this->env->getFunction('postEvent')->getCallable(), array("Live.renderVisitorIcons", $context["visitor"]));
            echo "
                ";
            // line 20
            if ( !($context["userIsAnonymous"] ?? $this->getContext($context, "userIsAnonymous"))) {
                // line 21
                echo "                <a class=\"visits-live-launch-visitor-profile rightLink\" title=\"";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Live_ViewVisitorProfile")), "html", null, true);
                echo " ";
                if ( !twig_test_empty($this->getAttribute($context["visitor"], "getColumn", array(0 => "userId"), "method"))) {
                    echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["visitor"], "getColumn", array(0 => "userId"), "method"), "html", null, true);
                }
                echo "\" data-visitor-id=\"";
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["visitor"], "getColumn", array(0 => "visitorId"), "method"), "html", null, true);
                echo "\">
                    <span class=\"icon-visitor-profile\"></span>
                </a>
                ";
            }
            // line 25
            echo "
                <span class=\"referrer\">
                    ";
            // line 27
            $this->loadTemplate("@Referrers/_visitorDetails.twig", "@Live/getLastVisitsStart.twig", 27)->display(array_merge($context, array("visitInfo" => $context["visitor"])));
            // line 28
            echo "                 </span>
            </div>
            <div id=\"";
            // line 30
            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["visitor"], "getColumn", array(0 => "idVisit"), "method"), "html_attr");
            echo "_actions\" class=\"settings\">
                <span class=\"pagesTitle\"
                      title=\"";
            // line 32
            echo \Piwik\piwik_escape_filter($this->env, twig_length_filter($this->env, $this->getAttribute($context["visitor"], "getColumn", array(0 => "actionDetails"), "method")), "html", null, true);
            echo " ";
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Actions")), "html", null, true);
            echo "\"
                      >";
            // line 33
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Actions")), "html", null, true);
            echo ":</span>&nbsp;
                ";
            // line 34
            $context["col"] = 0;
            // line 35
            echo "                ";
            $context['_parent'] = $context;
            $context['_seq'] = twig_ensure_traversable($this->getAttribute($context["visitor"], "getColumn", array(0 => "actionDetails"), "method"));
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
            foreach ($context['_seq'] as $context["_key"] => $context["action"]) {
                // line 36
                echo "                    ";
                if (($this->getAttribute($context["loop"], "index", array()) <= ($context["maxPagesDisplayedByVisitor"] ?? $this->getContext($context, "maxPagesDisplayedByVisitor")))) {
                    // line 37
                    echo "
                        ";
                    // line 38
                    if ((($this->getAttribute($context["action"], "type", array()) == "ecommerceOrder") || ($this->getAttribute($context["action"], "type", array()) == "ecommerceAbandonedCart"))) {
                        // line 39
                        echo "                            ";
                        ob_start();
                        // line 40
                        if (($this->getAttribute($context["action"], "type", array()) == "ecommerceOrder")) {
                            // line 41
                            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Goals_EcommerceOrder")), "html", null, true);
                        } else {
                            // line 43
                            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Goals_AbandonedCart")), "html", null, true);
                        }
                        // line 45
                        echo "
 - ";
                        // line 46
                        if (($this->getAttribute($context["action"], "type", array()) == "ecommerceOrder")) {
                            // line 47
                            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_ColumnRevenue")), "html", null, true);
                            echo ":";
                        } else {
                            // line 49
                            ob_start();
                            // line 50
                            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_ColumnRevenue")), "html", null, true);
                            $context["revenueLeft"] = ('' === $tmp = ob_get_clean()) ? '' : new Twig_Markup($tmp, $this->env->getCharset());
                            // line 52
                            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Goals_LeftInCart", ($context["revenueLeft"] ?? $this->getContext($context, "revenueLeft")))), "html", null, true);
                            echo ":";
                        }
                        // line 53
                        echo " ";
                        echo call_user_func_array($this->env->getFilter('money')->getCallable(), array($this->getAttribute($context["action"], "revenue", array()), ($context["idSite"] ?? $this->getContext($context, "idSite"))));
                        // line 55
                        echo "
 - ";
                        echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["action"], "serverTimePretty", array()), "html", null, true);
                        // line 56
                        echo "
";
                        // line 57
                        if ( !twig_test_empty($this->getAttribute($context["action"], "itemDetails", array()))) {
                            // line 58
                            $context['_parent'] = $context;
                            $context['_seq'] = twig_ensure_traversable($this->getAttribute($context["action"], "itemDetails", array()));
                            foreach ($context['_seq'] as $context["_key"] => $context["product"]) {
                                // line 59
                                echo "
# ";
                                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["product"], "itemSKU", array()), "html", null, true);
                                if ( !twig_test_empty($this->getAttribute($context["product"], "itemName", array()))) {
                                    echo ": ";
                                    echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["product"], "itemName", array()), "html", null, true);
                                }
                                if ( !twig_test_empty($this->getAttribute($context["product"], "itemCategory", array()))) {
                                    echo " (";
                                    echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["product"], "itemCategory", array()), "html", null, true);
                                    echo ")";
                                }
                                echo ", ";
                                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Quantity")), "html", null, true);
                                echo ": ";
                                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["product"], "quantity", array()), "html", null, true);
                                echo ", ";
                                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Price")), "html", null, true);
                                echo ": ";
                                echo call_user_func_array($this->env->getFilter('money')->getCallable(), array($this->getAttribute($context["product"], "price", array()), ($context["idSite"] ?? $this->getContext($context, "idSite"))));
                            }
                            $_parent = $context['_parent'];
                            unset($context['_seq'], $context['_iterated'], $context['_key'], $context['product'], $context['_parent'], $context['loop']);
                            $context = array_intersect_key($context, $_parent) + $_parent;
                        }
                        // line 62
                        echo "                            ";
                        $context["title"] = ('' === $tmp = ob_get_clean()) ? '' : new Twig_Markup($tmp, $this->env->getCharset());
                        // line 63
                        echo "                            <span title=\"";
                        echo \Piwik\piwik_escape_filter($this->env, ($context["title"] ?? $this->getContext($context, "title")), "html", null, true);
                        echo "\">
\t\t\t\t\t\t        <img class='iconPadding' src=\"";
                        // line 64
                        echo \Piwik\piwik_escape_filter($this->env, (($this->getAttribute($context["action"], "iconSVG", array(), "any", true, true)) ? (_twig_default_filter($this->getAttribute($context["action"], "iconSVG", array()), $this->getAttribute($context["action"], "icon", array()))) : ($this->getAttribute($context["action"], "icon", array()))), "html", null, true);
                        echo "\"/>
                                ";
                        // line 65
                        if (($this->getAttribute($context["action"], "type", array()) == "ecommerceOrder")) {
                            // line 66
                            echo "                                    ";
                            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_ColumnRevenue")), "html", null, true);
                            echo ": ";
                            echo call_user_func_array($this->env->getFilter('money')->getCallable(), array($this->getAttribute($context["action"], "revenue", array()), ($context["idSite"] ?? $this->getContext($context, "idSite"))));
                            echo "
                                ";
                        }
                        // line 68
                        echo "                            </span>

                        ";
                    } else {
                        // line 71
                        echo "
                            ";
                        // line 72
                        if (($this->getAttribute($context["action"], "url", array(), "any", true, true) &&  !twig_test_empty($this->getAttribute($context["action"], "url", array())))) {
                            // line 73
                            echo "                            <a href=\"";
                            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('safelink')->getCallable(), array($this->getAttribute($context["action"], "url", array()))), "html_attr");
                            echo "\" target=\"_blank\">
                            ";
                        }
                        // line 75
                        echo "                                ";
                        if (($this->getAttribute($context["action"], "type", array()) == "action")) {
                            // line 77
                            ob_start();
                            // line 78
                            if ( !twig_test_empty(twig_trim_filter($this->getAttribute($context["action"], "url", array())))) {
                                echo "<span class='tooltip-action-url'>";
                                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["action"], "url", array()), "html", null, true);
                                echo "</span>";
                            }
                            // line 79
                            echo "
";
                            // line 80
                            if ( !twig_test_empty($this->getAttribute($context["action"], "pageTitle", array()))) {
                                echo "<span class='tooltip-action-page-title'>";
                                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["action"], "pageTitle", array()), "html", null, true);
                                echo "</span>";
                            }
                            // line 81
                            echo "
<span class=\"tooltip-action-server-time\">";
                            // line 82
                            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["action"], "serverTimePretty", array()), "html", null, true);
                            echo "</span>
    ";
                            // line 83
                            if ($this->getAttribute($context["action"], "timeSpentPretty", array(), "any", true, true)) {
                                echo "<span class='tooltip-time-on-page'>";
                                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_TimeOnPage")), "html", null, true);
                                echo ": ";
                                echo $this->getAttribute($context["action"], "timeSpentPretty", array());
                                echo "</span>";
                            }
                            $context["title"] = ('' === $tmp = ob_get_clean()) ? '' : new Twig_Markup($tmp, $this->env->getCharset());
                            // line 85
                            echo "                                    <img class='iconPadding' src=\"";
                            echo \Piwik\piwik_escape_filter($this->env, (($this->getAttribute($context["action"], "iconSVG", array(), "any", true, true)) ? (_twig_default_filter($this->getAttribute($context["action"], "iconSVG", array()), $this->getAttribute($context["action"], "icon", array()))) : ($this->getAttribute($context["action"], "icon", array()))), "html", null, true);
                            echo "\" title=\"";
                            echo \Piwik\piwik_escape_filter($this->env, ($context["title"] ?? $this->getContext($context, "title")), "html_attr");
                            echo "\"/>
                                ";
                        } elseif ((($this->getAttribute(                        // line 86
$context["action"], "type", array()) == "outlink") || ($this->getAttribute($context["action"], "type", array()) == "download"))) {
                            // line 87
                            echo "                                    <img class='iconPadding' src=\"";
                            echo \Piwik\piwik_escape_filter($this->env, (($this->getAttribute($context["action"], "iconSVG", array(), "any", true, true)) ? (_twig_default_filter($this->getAttribute($context["action"], "iconSVG", array()), $this->getAttribute($context["action"], "icon", array()))) : ($this->getAttribute($context["action"], "icon", array()))), "html", null, true);
                            echo "\"
                                         title=\"";
                            // line 88
                            if ($this->getAttribute($context["action"], "url", array(), "any", true, true)) {
                                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["action"], "url", array()), "html", null, true);
                                echo " - ";
                            }
                            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["action"], "serverTimePretty", array()), "html", null, true);
                            echo "\"/>
                                ";
                        } elseif (($this->getAttribute(                        // line 89
$context["action"], "type", array()) == "search")) {
                            // line 90
                            echo "                                    <img class='iconPadding' src=\"";
                            echo \Piwik\piwik_escape_filter($this->env, (($this->getAttribute($context["action"], "iconSVG", array(), "any", true, true)) ? (_twig_default_filter($this->getAttribute($context["action"], "iconSVG", array()), $this->getAttribute($context["action"], "icon", array()))) : ($this->getAttribute($context["action"], "icon", array()))), "html", null, true);
                            echo "\"
                                         title=\"";
                            // line 91
                            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Actions_SubmenuSitesearch")), "html", null, true);
                            echo ": ";
                            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["action"], "siteSearchKeyword", array()), "html", null, true);
                            echo " - ";
                            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["action"], "serverTimePretty", array()), "html", null, true);
                            echo "\"/>
                                ";
                        } elseif ( !twig_test_empty((($this->getAttribute(                        // line 92
$context["action"], "eventCategory", array(), "any", true, true)) ? (_twig_default_filter($this->getAttribute($context["action"], "eventCategory", array()), false)) : (false)))) {
                            // line 93
                            echo "                                    <img  class=\"iconPadding\" src='";
                            echo \Piwik\piwik_escape_filter($this->env, (($this->getAttribute($context["action"], "iconSVG", array(), "any", true, true)) ? (_twig_default_filter($this->getAttribute($context["action"], "iconSVG", array()), $this->getAttribute($context["action"], "icon", array()))) : ($this->getAttribute($context["action"], "icon", array()))), "html", null, true);
                            echo "'
                                        title=\"";
                            // line 94
                            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Events_Event")), "html", null, true);
                            echo " ";
                            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["action"], "eventCategory", array()), "html", null, true);
                            echo " - ";
                            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["action"], "eventAction", array()), "html", null, true);
                            echo " ";
                            if ($this->getAttribute($context["action"], "eventName", array(), "any", true, true)) {
                                echo "- ";
                                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["action"], "eventName", array()), "html", null, true);
                            }
                            echo " ";
                            if ($this->getAttribute($context["action"], "eventValue", array(), "any", true, true)) {
                                echo "- ";
                                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["action"], "eventValue", array()), "html", null, true);
                            }
                            echo "\"/>
                                ";
                        } elseif (((($this->getAttribute(                        // line 95
$context["action"], "type", array()) == "goal") || ($this->getAttribute($context["action"], "type", array()) == twig_constant("Piwik\\Piwik::LABEL_ID_GOAL_IS_ECOMMERCE_ORDER"))) || ($this->getAttribute(                        // line 96
$context["action"], "type", array()) == twig_constant("Piwik\\Piwik::LABEL_ID_GOAL_IS_ECOMMERCE_CART")))) {
                            // line 97
                            echo "                                    <img class='iconPadding' src=\"";
                            echo \Piwik\piwik_escape_filter($this->env, (($this->getAttribute($context["action"], "iconSVG", array(), "any", true, true)) ? (_twig_default_filter($this->getAttribute($context["action"], "iconSVG", array()), $this->getAttribute($context["action"], "icon", array()))) : ($this->getAttribute($context["action"], "icon", array()))), "html", null, true);
                            echo "\"
                                         title=\"";
                            // line 98
                            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["action"], "goalName", array()), "html", null, true);
                            echo " - ";
                            if (($this->getAttribute($context["action"], "revenue", array()) > 0)) {
                                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_ColumnRevenue")), "html", null, true);
                                echo ": ";
                                echo call_user_func_array($this->env->getFilter('money')->getCallable(), array($this->getAttribute($context["action"], "revenue", array()), ($context["idSite"] ?? $this->getContext($context, "idSite"))));
                                echo " - ";
                            }
                            echo " ";
                            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["action"], "serverTimePretty", array()), "html", null, true);
                            echo "\"/>
                                ";
                        }
                        // line 100
                        echo "                            ";
                        if (($this->getAttribute($context["action"], "url", array(), "any", true, true) &&  !twig_test_empty($this->getAttribute($context["action"], "url", array())))) {
                            // line 101
                            echo "                            </a>
                            ";
                        }
                        // line 103
                        echo "                        ";
                    }
                    // line 104
                    echo "                    ";
                }
                // line 105
                echo "                ";
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
            unset($context['_seq'], $context['_iterated'], $context['_key'], $context['action'], $context['_parent'], $context['loop']);
            $context = array_intersect_key($context, $_parent) + $_parent;
            // line 106
            echo "
                ";
            // line 107
            if ((twig_length_filter($this->env, $this->getAttribute($context["visitor"], "getColumn", array(0 => "actionDetails"), "method")) > ($context["maxPagesDisplayedByVisitor"] ?? $this->getContext($context, "maxPagesDisplayedByVisitor")))) {
                // line 108
                echo "                    (";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Live_MorePagesNotDisplayed")), "html", null, true);
                echo ")
                ";
            }
            // line 110
            echo "            </div>
        </li>
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
        unset($context['_seq'], $context['_iterated'], $context['_key'], $context['visitor'], $context['_parent'], $context['loop']);
        $context = array_intersect_key($context, $_parent) + $_parent;
        // line 113
        echo "</ul>
<script type=\"text/javascript\">
\$(function () {
    \$('#visitsLive').on('click', '.visits-live-launch-visitor-profile', function (e) {
        e.preventDefault();
        broadcast.propagateNewPopoverParameter('visitorProfile', \$(this).attr('data-visitor-id'));
        return false;
    }).tooltip({
        track: true,
        content: function() {
            var title = \$(this).attr('title');
            return piwikHelper.escape(title.replace(/\\n/g, '<br />'));
        },
        show: {delay: 100, duration: 0},
        hide: false
    });
});
</script>
";
    }

    public function getTemplateName()
    {
        return "@Live/getLastVisitsStart.twig";
    }

    public function isTraitable()
    {
        return false;
    }

    public function getDebugInfo()
    {
        return array (  458 => 113,  442 => 110,  436 => 108,  434 => 107,  431 => 106,  417 => 105,  414 => 104,  411 => 103,  407 => 101,  404 => 100,  390 => 98,  385 => 97,  383 => 96,  382 => 95,  364 => 94,  359 => 93,  357 => 92,  349 => 91,  344 => 90,  342 => 89,  334 => 88,  329 => 87,  327 => 86,  320 => 85,  311 => 83,  307 => 82,  304 => 81,  298 => 80,  295 => 79,  289 => 78,  287 => 77,  284 => 75,  278 => 73,  276 => 72,  273 => 71,  268 => 68,  260 => 66,  258 => 65,  254 => 64,  249 => 63,  246 => 62,  220 => 59,  216 => 58,  214 => 57,  211 => 56,  207 => 55,  204 => 53,  200 => 52,  197 => 50,  195 => 49,  191 => 47,  189 => 46,  186 => 45,  183 => 43,  180 => 41,  178 => 40,  175 => 39,  173 => 38,  170 => 37,  167 => 36,  149 => 35,  147 => 34,  143 => 33,  137 => 32,  132 => 30,  128 => 28,  126 => 27,  122 => 25,  108 => 21,  106 => 20,  102 => 19,  99 => 18,  93 => 15,  82 => 14,  80 => 13,  67 => 12,  65 => 11,  61 => 10,  57 => 9,  51 => 8,  47 => 7,  42 => 6,  25 => 5,  21 => 3,  19 => 2,);
    }

    /** @deprecated since 1.27 (to be removed in 2.0). Use getSourceContext() instead */
    public function getSource()
    {
        @trigger_error('The '.__METHOD__.' method is deprecated since version 1.27 and will be removed in 2.0. Use getSourceContext() instead.', E_USER_DEPRECATED);

        return $this->getSourceContext()->getCode();
    }

    public function getSourceContext()
    {
        return new Twig_Source("{# some users view thousands of pages which can crash the browser viewing Live! #}
{% set maxPagesDisplayedByVisitor=100 %}

<ul id=\"visitsLive\">
    {% for visitor in visitors.getRows() %}
        <li id=\"{{ visitor.getColumn('idVisit') }}\" class=\"visit\">
            <div style=\"display:none;\" class=\"idvisit\">{{ visitor.idVisit }}</div>
            <div title=\"{{ visitor.getColumn('actionDetails')|length }} {{ 'General_Actions'|translate }}\" class=\"datetime\">
                <span style=\"display:none;\" class=\"serverTimestamp\">{{ visitor.getColumn('serverTimestamp')|raw }}</span>
                {{ postEvent('Live.visitorLogWidgetViewBeforeVisitInfo', visitor) }}
                {% set year = visitor.getColumn('serverTimestamp')|date('Y') %}
                <span class=\"realTimeWidget_datetime\">{{ visitor.getColumn('serverDatePretty')|replace({(year): ' '}) }} - {{ visitor.getColumn('serverTimePretty') }} {% if visitor.getColumn('visitDuration') > 0 %}({{ visitor.getColumn('visitDurationPretty')|raw }}){% endif %}</span>
                {% if visitor.getColumn('userId')|default(false) is not empty %}
                  &nbsp;  <a class=\"visits-live-launch-visitor-profile rightLink\" title=\"{{ 'Live_ViewVisitorProfile'|translate }} {% if visitor.getColumn('userId') is not empty %}{{ visitor.getColumn('userId') }}{% endif %}\" data-visitor-id=\"{{ visitor.getColumn('visitorId') }}\">
                        <span>{{ visitor.getColumn('userId')|rawSafeDecoded}}</span>
                    </a>
                {% endif %}

                {{ postEvent('Live.renderVisitorIcons', visitor) }}
                {% if not userIsAnonymous %}
                <a class=\"visits-live-launch-visitor-profile rightLink\" title=\"{{ 'Live_ViewVisitorProfile'|translate }} {% if visitor.getColumn('userId') is not empty %}{{ visitor.getColumn('userId') }}{% endif %}\" data-visitor-id=\"{{ visitor.getColumn('visitorId') }}\">
                    <span class=\"icon-visitor-profile\"></span>
                </a>
                {% endif %}

                <span class=\"referrer\">
                    {% include \"@Referrers/_visitorDetails.twig\" with {'visitInfo': visitor} %}
                 </span>
            </div>
            <div id=\"{{ visitor.getColumn('idVisit')|e('html_attr') }}_actions\" class=\"settings\">
                <span class=\"pagesTitle\"
                      title=\"{{ visitor.getColumn('actionDetails')|length }} {{ 'General_Actions'|translate }}\"
                      >{{ 'General_Actions'|translate }}:</span>&nbsp;
                {% set col = 0 %}
                {% for action in visitor.getColumn('actionDetails') %}
                    {% if loop.index <= maxPagesDisplayedByVisitor %}

                        {% if action.type == 'ecommerceOrder' or action.type == 'ecommerceAbandonedCart' %}
                            {% set title %}
                                {%- if action.type == 'ecommerceOrder' %}
                                    {{- 'Goals_EcommerceOrder'|translate -}}
                                {% else %}
                                    {{- 'Goals_AbandonedCart'|translate -}}
                                {% endif %}
                                {{- \"\\n - \" -}}
                                {%- if action.type == 'ecommerceOrder' -%}
                                    {{- 'General_ColumnRevenue'|translate -}}:
                                  {%- else -%}
                                    {%- set revenueLeft -%}
                                        {{- 'General_ColumnRevenue'|translate -}}
                                    {%- endset -%}
                                    {{- 'Goals_LeftInCart'|translate(revenueLeft) -}}:
                                {%- endif %} {{ action.revenue|money(idSite)|raw -}}

                                {{- \"\\n - \" -}}{{- action.serverTimePretty -}}
                                {{- \"\\n\" -}}
                                {% if action.itemDetails is not empty -%}
                                    {% for product in action.itemDetails -%}
                                        {{- \"\\n# \" -}}{{ product.itemSKU }}{% if product.itemName is not empty %}: {{ product.itemName }}{% endif %}{% if product.itemCategory is not empty %} ({{ product.itemCategory }}){% endif %}, {{ 'General_Quantity'|translate }}: {{ product.quantity }}, {{ 'General_Price'|translate }}: {{ product.price|money(idSite)|raw }}
                                    {%- endfor %}
                                {%- endif %}
                            {% endset %}
                            <span title=\"{{- title -}}\">
\t\t\t\t\t\t        <img class='iconPadding' src=\"{{ action.iconSVG|default(action.icon) }}\"/>
                                {% if action.type == 'ecommerceOrder' %}
                                    {{ 'General_ColumnRevenue'|translate }}: {{ action.revenue|money(idSite)|raw }}
                                {% endif %}
                            </span>

                        {% else %}

                            {% if action.url is defined and action.url is not empty %}
                            <a href=\"{{ action.url|safelink|e('html_attr') }}\" target=\"_blank\">
                            {% endif %}
                                {% if action.type == 'action' %}
{# white spacing matters as Chrome tooltip display whitespaces #}
{% set title %}
{% if action.url|trim is not empty %}<span class='tooltip-action-url'>{{ action.url }}</span>{% endif %}

{% if action.pageTitle is not empty %}<span class='tooltip-action-page-title'>{{ action.pageTitle }}</span>{% endif %}

<span class=\"tooltip-action-server-time\">{{ action.serverTimePretty }}</span>
    {% if action.timeSpentPretty is defined %}<span class='tooltip-time-on-page'>{{ 'General_TimeOnPage'|translate }}: {{ action.timeSpentPretty|raw }}</span>{% endif %}
{%- endset %}
                                    <img class='iconPadding' src=\"{{ action.iconSVG|default(action.icon) }}\" title=\"{{- title|e('html_attr') -}}\"/>
                                {% elseif action.type == 'outlink' or action.type == 'download' %}
                                    <img class='iconPadding' src=\"{{ action.iconSVG|default(action.icon) }}\"
                                         title=\"{% if action.url is defined %}{{ action.url }} - {% endif %}{{ action.serverTimePretty }}\"/>
                                {% elseif action.type == 'search' %}
                                    <img class='iconPadding' src=\"{{ action.iconSVG|default(action.icon) }}\"
                                         title=\"{{ 'Actions_SubmenuSitesearch'|translate }}: {{ action.siteSearchKeyword }} - {{ action.serverTimePretty }}\"/>
                                {% elseif action.eventCategory|default(false) is not empty %}
                                    <img  class=\"iconPadding\" src='{{ action.iconSVG|default(action.icon) }}'
                                        title=\"{{ 'Events_Event'|translate }} {{ action.eventCategory }} - {{ action.eventAction }} {% if action.eventName is defined %}- {{ action.eventName }}{% endif %} {% if action.eventValue is defined %}- {{ action.eventValue }}{% endif %}\"/>
                                {% elseif action.type == 'goal' or action.type == constant('Piwik\\\\Piwik::LABEL_ID_GOAL_IS_ECOMMERCE_ORDER') or
                                          action.type == constant('Piwik\\\\Piwik::LABEL_ID_GOAL_IS_ECOMMERCE_CART') %}
                                    <img class='iconPadding' src=\"{{ action.iconSVG|default(action.icon) }}\"
                                         title=\"{{ action.goalName }} - {% if action.revenue > 0 %}{{ 'General_ColumnRevenue'|translate }}: {{ action.revenue|money(idSite)|raw }} - {% endif %} {{ action.serverTimePretty }}\"/>
                                {% endif %}
                            {% if action.url is defined and action.url is not empty %}
                            </a>
                            {% endif %}
                        {% endif %}
                    {% endif %}
                {% endfor %}

                {% if visitor.getColumn('actionDetails')|length > maxPagesDisplayedByVisitor %}
                    ({{ 'Live_MorePagesNotDisplayed'|translate }})
                {% endif %}
            </div>
        </li>
    {% endfor %}
</ul>
<script type=\"text/javascript\">
\$(function () {
    \$('#visitsLive').on('click', '.visits-live-launch-visitor-profile', function (e) {
        e.preventDefault();
        broadcast.propagateNewPopoverParameter('visitorProfile', \$(this).attr('data-visitor-id'));
        return false;
    }).tooltip({
        track: true,
        content: function() {
            var title = \$(this).attr('title');
            return piwikHelper.escape(title.replace(/\\n/g, '<br />'));
        },
        show: {delay: 100, duration: 0},
        hide: false
    });
});
</script>
", "@Live/getLastVisitsStart.twig", "D:\\xampp\\htdocs\\matomo\\plugins\\Live\\templates\\getLastVisitsStart.twig");
    }
}
