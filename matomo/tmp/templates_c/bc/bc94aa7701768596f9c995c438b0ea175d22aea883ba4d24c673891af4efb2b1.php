<?php

/* @CorePluginsAdmin/macros.twig */
class __TwigTemplate_88495c38df2509222bd47238b46eabb575ecc8b59b4230da50be04bcc2c56096 extends Twig_Template
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
        echo "
";
        // line 107
        echo "
";
        // line 117
        echo "
";
        // line 138
        echo "
";
    }

    // line 2
    public function gettablePluginUpdates($__pluginsHavingUpdate__ = null, $__updateNonce__ = null, $__isMultiServerEnvironment__ = null, ...$__varargs__)
    {
        $context = $this->env->mergeGlobals(array(
            "pluginsHavingUpdate" => $__pluginsHavingUpdate__,
            "updateNonce" => $__updateNonce__,
            "isMultiServerEnvironment" => $__isMultiServerEnvironment__,
            "varargs" => $__varargs__,
        ));

        $blocks = array();

        ob_start();
        try {
            // line 3
            echo "    ";
            $context["marketplaceMacro"] = $this->loadTemplate("@Marketplace/macros.twig", "@CorePluginsAdmin/macros.twig", 3);
            // line 4
            echo "
    <div>
        <a id=\"update-selected-plugins\" href=\"javascript:\" class=\"btn\">";
            // line 6
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_UpdateSelected")), "html", null, true);
            echo "</a>
    </div>
    <table piwik-content-table>
        <thead>
        <tr>
            <th>
                <span class=\"checkbox-container\">
                    <input type=\"checkbox\" id=\"select-plugin-all\" />
                    <label for=\"select-plugin-all\"></label>
                </span>
            </th>
            <th>";
            // line 17
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Plugin")), "html", null, true);
            echo "</th>
            <th class=\"num\">";
            // line 18
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_Version")), "html", null, true);
            echo "</th>
            <th>";
            // line 19
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Description")), "html", null, true);
            echo "</th>
            <th class=\"status\">";
            // line 20
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_Status")), "html", null, true);
            echo "</th>
            <th class=\"action-links\">";
            // line 21
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Action")), "html", null, true);
            echo "</th>
        </tr>
        </thead>
        <tbody id=\"plugins\">
        ";
            // line 25
            $context['_parent'] = $context;
            $context['_seq'] = twig_ensure_traversable(($context["pluginsHavingUpdate"] ?? $this->getContext($context, "pluginsHavingUpdate")));
            foreach ($context['_seq'] as $context["name"] => $context["plugin"]) {
                // line 26
                echo "            <tr ";
                if ((($this->getAttribute($context["plugin"], "isActivated", array(), "any", true, true)) ? (_twig_default_filter($this->getAttribute($context["plugin"], "isActivated", array()), false)) : (false))) {
                    echo "class=\"active-plugin\"";
                } else {
                    echo "class=\"inactive-plugin\"";
                }
                echo ">
                <td class=\"select-cell\">
                    <span class=\"checkbox-container\">
                        <input type=\"checkbox\" id=\"select-plugin-";
                // line 29
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["plugin"], "name", array()), "html_attr");
                echo "\" ";
                if (($this->getAttribute($context["plugin"], "isDownloadable", array(), "any", true, true) &&  !$this->getAttribute($context["plugin"], "isDownloadable", array()))) {
                    echo "disabled=\"disabled\"";
                }
                echo " />
                        <label for=\"select-plugin-";
                // line 30
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["plugin"], "name", array()), "html_attr");
                echo "\"></label>
                    </span>
                </td>
                <td class=\"name\">
                    <a href=\"javascript:void(0);\" piwik-plugin-name=\"";
                // line 34
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["plugin"], "name", array()), "html_attr");
                echo "\" class=\"plugin-details\">
                        ";
                // line 35
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["plugin"], "name", array()), "html", null, true);
                echo "
                    </a>
                </td>
                <td class=\"vers\">
                    ";
                // line 39
                if (((($this->getAttribute($context["plugin"], "changelog", array(), "any", true, true) && $this->getAttribute($context["plugin"], "changelog", array())) && $this->getAttribute($this->getAttribute($context["plugin"], "changelog", array(), "any", false, true), "url", array(), "any", true, true)) && $this->getAttribute($this->getAttribute($context["plugin"], "changelog", array()), "url", array()))) {
                    // line 40
                    echo "                        <a href=\"";
                    echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($this->getAttribute($context["plugin"], "changelog", array()), "url", array()), "html_attr");
                    echo "\" title=\"";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_Changelog")), "html", null, true);
                    echo "\"
                           target=\"_blank\" rel=\"noreferrer noopener\"
                        >";
                    // line 42
                    echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["plugin"], "currentVersion", array()), "html", null, true);
                    echo " => ";
                    echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["plugin"], "latestVersion", array()), "html", null, true);
                    echo "</a>
                    ";
                } else {
                    // line 44
                    echo "                        ";
                    echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["plugin"], "currentVersion", array()), "html", null, true);
                    echo " => ";
                    echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["plugin"], "latestVersion", array()), "html", null, true);
                    echo "
                    ";
                }
                // line 46
                echo "                </td>
                <td class=\"desc\">
                    ";
                // line 48
                echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["plugin"], "description", array()), "html", null, true);
                echo "
                    ";
                // line 49
                echo $context["marketplaceMacro"]->getmissingRequirementsPleaseUpdateNotice($context["plugin"]);
                echo "
                </td>
                <td class=\"status\">
                    ";
                // line 52
                if ($this->getAttribute($context["plugin"], "isActivated", array())) {
                    // line 53
                    echo "                        ";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_Active")), "html", null, true);
                    echo "
                    ";
                } else {
                    // line 55
                    echo "                        ";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_Inactive")), "html", null, true);
                    echo "
                    ";
                }
                // line 57
                echo "                </td>
                <td class=\"togl action-links\">
                    ";
                // line 59
                if (($this->getAttribute($context["plugin"], "isDownloadable", array(), "any", true, true) &&  !$this->getAttribute($context["plugin"], "isDownloadable", array()))) {
                    // line 60
                    echo "                        <span title=\"";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_PluginNotDownloadable")), "html_attr");
                    echo " ";
                    if ($this->getAttribute($context["plugin"], "isPaid", array())) {
                        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_PluginNotDownloadablePaidReason")), "html_attr");
                    }
                    echo "\"
                          >";
                    // line 61
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_NotDownloadable")), "html_attr");
                    echo "</span>
                    ";
                } elseif (                // line 62
($context["isMultiServerEnvironment"] ?? $this->getContext($context, "isMultiServerEnvironment"))) {
                    // line 63
                    echo "                        <a onclick=\"\$(this).css('display', 'none')\" href=\"";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFunction('linkTo')->getCallable(), array(array("action" => "download", "module" => "Marketplace", "pluginName" => $this->getAttribute($context["plugin"], "name", array()), "nonce" => Piwik\Nonce::getNonce($this->getAttribute($context["plugin"], "name", array()))))), "html", null, true);
                    echo "\">";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Download")), "html", null, true);
                    echo "</a>
                    ";
                } elseif ((0 == twig_length_filter($this->env, $this->getAttribute(                // line 64
$context["plugin"], "missingRequirements", array())))) {
                    // line 65
                    echo "                        <a href=\"";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFunction('linkTo')->getCallable(), array(array("action" => "updatePlugin", "module" => "Marketplace", "pluginName" => $this->getAttribute($context["plugin"], "name", array()), "nonce" => ($context["updateNonce"] ?? $this->getContext($context, "updateNonce"))))), "html", null, true);
                    echo "\">";
                    echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CoreUpdater_UpdateTitle")), "html", null, true);
                    echo "</a>
                    ";
                } else {
                    // line 67
                    echo "                        -
                    ";
                }
                // line 69
                echo "                </td>
            </tr>
        ";
            }
            $_parent = $context['_parent'];
            unset($context['_seq'], $context['_iterated'], $context['name'], $context['plugin'], $context['_parent'], $context['loop']);
            $context = array_intersect_key($context, $_parent) + $_parent;
            // line 72
            echo "        </tbody>
    </table>

    <script>
        \$(function () {
            \$('#update-selected-plugins').on('click', function () {
                var pluginsToUpdate = [];
                \$('tbody#plugins td.select-cell input').each(function () {
                    if (!this.checked) {
                        return;
                    }

                    var pluginName = \$(this).closest('tr').find('.name .plugin-details').attr('piwik-plugin-name');
                    pluginsToUpdate.push(pluginName);
                });

                var url = '";
            // line 88
            echo call_user_func_array($this->env->getFunction('linkTo')->getCallable(), array(array("module" => "Marketplace", "action" => "updatePlugin", "nonce" => ($context["updateNonce"] ?? $this->getContext($context, "updateNonce")))));
            echo "&pluginName=' + encodeURIComponent(pluginsToUpdate.join(','));
                window.location.href = url;

                \$(this).prop('disabled', true);
            });

            \$('#select-plugin-all').on('change', function () {
                var self = this;
                \$('tbody#plugins td.select-cell input[type=checkbox]').each(function () {
                    if (\$(this).prop('disabled')) {
                        return;
                    }
                    \$(this).prop('checked', self.checked);
                });
            });
        });
    </script>

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

    // line 108
    public function getpluginActivateDeactivateAction($__name__ = null, $__isActivated__ = null, $__missingRequirements__ = null, $__deactivateNonce__ = null, $__activateNonce__ = null, ...$__varargs__)
    {
        $context = $this->env->mergeGlobals(array(
            "name" => $__name__,
            "isActivated" => $__isActivated__,
            "missingRequirements" => $__missingRequirements__,
            "deactivateNonce" => $__deactivateNonce__,
            "activateNonce" => $__activateNonce__,
            "varargs" => $__varargs__,
        ));

        $blocks = array();

        ob_start();
        try {
            // line 109
            if (($context["isActivated"] ?? $this->getContext($context, "isActivated"))) {
                // line 110
                echo "<a href='index.php?module=CorePluginsAdmin&action=deactivate&pluginName=";
                echo \Piwik\piwik_escape_filter($this->env, ($context["name"] ?? $this->getContext($context, "name")), "html", null, true);
                echo "&nonce=";
                echo \Piwik\piwik_escape_filter($this->env, ($context["deactivateNonce"] ?? $this->getContext($context, "deactivateNonce")), "html", null, true);
                echo "&redirectTo=referrer'>";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_Deactivate")), "html", null, true);
                echo "</a>";
            } elseif (            // line 111
($context["missingRequirements"] ?? $this->getContext($context, "missingRequirements"))) {
                // line 112
                echo "        -
    ";
            } else {
                // line 114
                echo "<a href='index.php?module=CorePluginsAdmin&action=activate&pluginName=";
                echo \Piwik\piwik_escape_filter($this->env, ($context["name"] ?? $this->getContext($context, "name")), "html", null, true);
                echo "&nonce=";
                echo \Piwik\piwik_escape_filter($this->env, ($context["activateNonce"] ?? $this->getContext($context, "activateNonce")), "html", null, true);
                echo "&redirectTo=referrer'>";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_Activate")), "html", null, true);
                echo "</a>";
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

    // line 118
    public function getpluginsFilter(...$__varargs__)
    {
        $context = $this->env->mergeGlobals(array(
            "varargs" => $__varargs__,
        ));

        $blocks = array();

        ob_start();
        try {
            // line 119
            echo "
    <p class=\"row pluginsFilter\" piwik-plugin-filter>
        <span class=\"origin\">
            <strong>";
            // line 122
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_Origin")), "html", null, true);
            echo "</strong>
            <a data-filter-origin=\"all\" href=\"#\" class=\"active\">";
            // line 123
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_All")), "html", null, true);
            echo "<span class=\"counter\"></span></a> |
            <a data-filter-origin=\"core\" href=\"#\">";
            // line 124
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_OriginCore")), "html", null, true);
            echo "<span class=\"counter\"></span></a> |
            <a data-filter-origin=\"official\" href=\"#\">";
            // line 125
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_OriginOfficial")), "html", null, true);
            echo "<span class=\"counter\"></span></a> |
            <a data-filter-origin=\"thirdparty\" href=\"#\">";
            // line 126
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_OriginThirdParty")), "html", null, true);
            echo "<span class=\"counter\"></span></a>
        </span>

        <span class=\"status\">
            <strong>";
            // line 130
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_Status")), "html", null, true);
            echo "</strong>
            <a data-filter-status=\"all\" href=\"#\" class=\"active\">";
            // line 131
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_All")), "html", null, true);
            echo "<span class=\"counter\"></span></a> |
            <a data-filter-status=\"active\" href=\"#\">";
            // line 132
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_Active")), "html", null, true);
            echo "<span class=\"counter\"></span></a> |
            <a data-filter-status=\"inactive\" href=\"#\">";
            // line 133
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_Inactive")), "html", null, true);
            echo "<span class=\"counter\"></span></a>
        </span>
    </p>

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

    // line 139
    public function gettablePlugins($__pluginsInfo__ = null, $__pluginNamesHavingSettings__ = null, $__activateNonce__ = null, $__deactivateNonce__ = null, $__uninstallNonce__ = null, $__isTheme__ = null, $__marketplacePluginNames__ = null, $__displayAdminLinks__ = null, ...$__varargs__)
    {
        $context = $this->env->mergeGlobals(array(
            "pluginsInfo" => $__pluginsInfo__,
            "pluginNamesHavingSettings" => $__pluginNamesHavingSettings__,
            "activateNonce" => $__activateNonce__,
            "deactivateNonce" => $__deactivateNonce__,
            "uninstallNonce" => $__uninstallNonce__,
            "isTheme" => $__isTheme__,
            "marketplacePluginNames" => $__marketplacePluginNames__,
            "displayAdminLinks" => $__displayAdminLinks__,
            "varargs" => $__varargs__,
        ));

        $blocks = array();

        ob_start();
        try {
            // line 140
            echo "
    <div id=\"confirmUninstallPlugin\" class=\"ui-confirm\">

        <h2 id=\"uninstallPluginConfirm\">";
            // line 143
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_UninstallConfirm")), "html", null, true);
            echo "</h2>
        <input role=\"yes\" type=\"button\" value=\"";
            // line 144
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Yes")), "html", null, true);
            echo "\"/>
        <input role=\"no\" type=\"button\" value=\"";
            // line 145
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_No")), "html", null, true);
            echo "\"/>

    </div>

    <table piwik-content-table>
        <thead>
        <tr>
            <th>";
            // line 152
            if (($context["isTheme"] ?? $this->getContext($context, "isTheme"))) {
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_Theme")), "html", null, true);
            } else {
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Plugin")), "html", null, true);
            }
            echo "</th>
            <th>";
            // line 153
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Description")), "html", null, true);
            echo "</th>
            <th class=\"status\">";
            // line 154
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_Status")), "html", null, true);
            echo "</th>
            ";
            // line 155
            if (($context["displayAdminLinks"] ?? $this->getContext($context, "displayAdminLinks"))) {
                // line 156
                echo "                <th class=\"action-links\">";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Action")), "html", null, true);
                echo "</th>
            ";
            }
            // line 158
            echo "        </tr>
        </thead>
        <tbody id=\"plugins\">
        ";
            // line 161
            $context['_parent'] = $context;
            $context['_seq'] = twig_ensure_traversable(($context["pluginsInfo"] ?? $this->getContext($context, "pluginsInfo")));
            foreach ($context['_seq'] as $context["name"] => $context["plugin"]) {
                // line 162
                echo "            ";
                $context["isDefaultTheme"] = (($context["isTheme"] ?? $this->getContext($context, "isTheme")) && ($context["name"] == "Morpheus"));
                // line 163
                echo "            ";
                if ((($this->getAttribute($context["plugin"], "alwaysActivated", array(), "any", true, true) &&  !$this->getAttribute($context["plugin"], "alwaysActivated", array())) || ($context["isTheme"] ?? $this->getContext($context, "isTheme")))) {
                    // line 164
                    echo "                <tr ";
                    if ($this->getAttribute($context["plugin"], "activated", array())) {
                        echo "class=\"active-plugin\"";
                    } else {
                        echo "class=\"inactive-plugin\"";
                    }
                    echo " data-filter-status=\"";
                    if ($this->getAttribute($context["plugin"], "activated", array())) {
                        echo "active";
                    } else {
                        echo "inactive";
                    }
                    echo "\" data-filter-origin=\"";
                    if ($this->getAttribute($context["plugin"], "isCorePlugin", array())) {
                        echo "core";
                    } elseif ($this->getAttribute($context["plugin"], "isOfficialPlugin", array())) {
                        echo "official";
                    } else {
                        echo "thirdparty";
                    }
                    echo "\">
                    <td class=\"name\">
                        <a name=\"";
                    // line 166
                    echo \Piwik\piwik_escape_filter($this->env, $context["name"], "html_attr");
                    echo "\"></a>
                        ";
                    // line 167
                    if (( !$this->getAttribute($context["plugin"], "isCorePlugin", array()) && twig_in_filter($context["name"], ($context["marketplacePluginNames"] ?? $this->getContext($context, "marketplacePluginNames"))))) {
                        // line 168
                        echo "<a href=\"javascript:void(0);\"
                               piwik-plugin-name=\"";
                        // line 169
                        echo \Piwik\piwik_escape_filter($this->env, $context["name"], "html_attr");
                        echo "\"
                            >";
                        // line 170
                        echo \Piwik\piwik_escape_filter($this->env, $context["name"], "html", null, true);
                        echo "</a>";
                    } else {
                        // line 172
                        echo "                            ";
                        echo \Piwik\piwik_escape_filter($this->env, $context["name"], "html", null, true);
                        echo "
                        ";
                    }
                    // line 174
                    echo "                        <span class=\"plugin-version\" ";
                    if ($this->getAttribute($context["plugin"], "isCorePlugin", array())) {
                        echo "title=\"";
                        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_CorePluginTooltip")), "html", null, true);
                        echo "\"";
                    }
                    echo ">(";
                    if ($this->getAttribute($context["plugin"], "isCorePlugin", array())) {
                        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_OriginCore")), "html", null, true);
                    } else {
                        echo "v";
                        echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($this->getAttribute($context["plugin"], "info", array()), "version", array()), "html", null, true);
                    }
                    echo ")</span>

                        ";
                    // line 176
                    if (twig_in_filter($context["name"], ($context["pluginNamesHavingSettings"] ?? $this->getContext($context, "pluginNamesHavingSettings")))) {
                        // line 177
                        echo "                            <br /><br />
                            <a href=\"";
                        // line 178
                        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFunction('linkTo')->getCallable(), array(array("module" => "CoreAdminHome", "action" => "generalSettings"))), "html", null, true);
                        echo "#";
                        echo \Piwik\piwik_escape_filter($this->env, $context["name"], "html_attr");
                        echo "\" class=\"settingsLink\">";
                        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Settings")), "html", null, true);
                        echo "</a>
                        ";
                    }
                    // line 180
                    echo "                    </td>
                    <td class=\"desc\">
                        <div class=\"plugin-desc-missingrequirements\">
                            ";
                    // line 183
                    if (($this->getAttribute($context["plugin"], "missingRequirements", array(), "any", true, true) && $this->getAttribute($context["plugin"], "missingRequirements", array()))) {
                        // line 184
                        echo "                                ";
                        echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["plugin"], "missingRequirements", array()), "html", null, true);
                        echo "
                                <br />
                            ";
                    }
                    // line 187
                    echo "                        </div>
                        <div class=\"plugin-desc-text\">

                            ";
                    // line 190
                    echo nl2br(\Piwik\piwik_escape_filter($this->env, $this->getAttribute($this->getAttribute($context["plugin"], "info", array()), "description", array()), "html", null, true));
                    echo "

                            ";
                    // line 192
                    if (( !twig_test_empty((($this->getAttribute($this->getAttribute($context["plugin"], "info", array(), "any", false, true), "homepage", array(), "any", true, true)) ? (_twig_default_filter($this->getAttribute($this->getAttribute($context["plugin"], "info", array(), "any", false, true), "homepage", array()))) : (""))) && !twig_in_filter($this->getAttribute($this->getAttribute($context["plugin"], "info", array()), "homepage", array()), array(0 => "http://piwik.org", 1 => "http://www.piwik.org", 2 => "http://piwik.org/", 3 => "http://www.piwik.org/", 4 => "https://piwik.org", 5 => "https://www.piwik.org", 6 => "https://piwik.org/", 7 => "https://www.piwik.org/", 8 => "http://matomo.org", 9 => "http://www.matomo.org", 10 => "http://matomo.org/", 11 => "http://www.matomo.org/", 12 => "https://matomo.org", 13 => "https://www.matomo.org", 14 => "https://matomo.org/", 15 => "https://www.matomo.org/")))) {
                        // line 198
                        echo "                                <span class=\"plugin-homepage\">
                            <a target=\"_blank\" rel=\"noreferrer noopener\" href=\"";
                        // line 199
                        echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($this->getAttribute($context["plugin"], "info", array()), "homepage", array()), "html_attr");
                        echo "\">(";
                        echo twig_replace_filter(call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_PluginHomepage")), array(" " => "&nbsp;"));
                        echo ")</a>
                        </span>
                            ";
                    }
                    // line 202
                    echo "
                            ";
                    // line 203
                    if (($this->getAttribute($this->getAttribute($context["plugin"], "info", array(), "any", false, true), "donate", array(), "any", true, true) && twig_length_filter($this->env, $this->getAttribute($this->getAttribute($context["plugin"], "info", array()), "donate", array())))) {
                        // line 204
                        echo "                                <div class=\"plugin-donation\">
                                    ";
                        // line 205
                        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_LikeThisPlugin")), "html", null, true);
                        echo " <a href=\"javascript:;\" class=\"plugin-donation-link\" data-overlay-id=\"overlay-";
                        echo \Piwik\piwik_escape_filter($this->env, $context["name"], "html_attr");
                        echo "\">";
                        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_ConsiderDonating")), "html", null, true);
                        echo "</a>
                                    <div id=\"overlay-";
                        // line 206
                        echo \Piwik\piwik_escape_filter($this->env, $context["name"], "html_attr");
                        echo "\" class=\"donation-overlay ui-confirm\" title=\"";
                        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_LikeThisPlugin")), "html", null, true);
                        echo "\">
                                        <p>";
                        // line 207
                        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_CommunityContributedPlugin")), "html", null, true);
                        echo "</p>
                                        <p>";
                        // line 208
                        echo call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_ConsiderDonatingCreatorOf", (("<b>" . $context["name"]) . "</b>")));
                        echo "</p>
                                        <div class=\"donation-links\">
                                            ";
                        // line 210
                        if (($this->getAttribute($this->getAttribute($this->getAttribute($context["plugin"], "info", array(), "any", false, true), "donate", array(), "any", false, true), "paypal", array(), "any", true, true) && $this->getAttribute($this->getAttribute($this->getAttribute($context["plugin"], "info", array()), "donate", array()), "paypal", array()))) {
                            // line 211
                            echo "                                                <a class=\"donation-link paypal\" target=\"_blank\" rel=\"noreferrer noopener\" href=\"https://www.paypal.com/cgi-bin/webscr?cmd=_donations&item_name=Matomo%20Plugin%20";
                            echo \Piwik\piwik_escape_filter($this->env, \Piwik\piwik_escape_filter($this->env, $context["name"], "url"), "html", null, true);
                            echo "&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted&business=";
                            echo \Piwik\piwik_escape_filter($this->env, \Piwik\piwik_escape_filter($this->env, $this->getAttribute($this->getAttribute($this->getAttribute($context["plugin"], "info", array()), "donate", array()), "paypal", array()), "url"), "html", null, true);
                            echo "\"><img src=\"plugins/CorePluginsAdmin/images/paypal_donate.png\" height=\"30\"/></a>
                                            ";
                        }
                        // line 213
                        echo "                                            ";
                        if (($this->getAttribute($this->getAttribute($this->getAttribute($context["plugin"], "info", array(), "any", false, true), "donate", array(), "any", false, true), "flattr", array(), "any", true, true) && $this->getAttribute($this->getAttribute($this->getAttribute($context["plugin"], "info", array()), "donate", array()), "flattr", array()))) {
                            // line 214
                            echo "                                                <a class=\"donation-link flattr\" target=\"_blank\" rel=\"noreferrer noopener\" href=\"";
                            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($this->getAttribute($this->getAttribute($context["plugin"], "info", array()), "donate", array()), "flattr", array()), "html", null, true);
                            echo "\"><img class=\"alignnone\" title=\"Flattr\" alt=\"\" src=\"plugins/CorePluginsAdmin/images/flattr.png\" height=\"29\" /></a>
                                            ";
                        }
                        // line 216
                        echo "                                            ";
                        if (($this->getAttribute($this->getAttribute($this->getAttribute($context["plugin"], "info", array(), "any", false, true), "donate", array(), "any", false, true), "bitcoin", array(), "any", true, true) && $this->getAttribute($this->getAttribute($this->getAttribute($context["plugin"], "info", array()), "donate", array()), "bitcoin", array()))) {
                            // line 217
                            echo "                                                <div class=\"donation-link bitcoin\">
                                                    <span>Donate Bitcoins to:</span>
                                                    <a href=\"bitcoin:";
                            // line 219
                            echo \Piwik\piwik_escape_filter($this->env, \Piwik\piwik_escape_filter($this->env, $this->getAttribute($this->getAttribute($this->getAttribute($context["plugin"], "info", array()), "donate", array()), "bitcoin", array()), "url"), "html", null, true);
                            echo "\">";
                            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($this->getAttribute($this->getAttribute($context["plugin"], "info", array()), "donate", array()), "bitcoin", array()), "html", null, true);
                            echo "</a>
                                                </div>
                                            ";
                        }
                        // line 222
                        echo "                                        </div>
                                        <input role=\"no\" type=\"button\" value=\"";
                        // line 223
                        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Close")), "html", null, true);
                        echo "\"/>
                                    </div>
                                </div>
                            ";
                    }
                    // line 227
                    echo "                        </div>
                        ";
                    // line 228
                    if ($this->getAttribute($this->getAttribute($context["plugin"], "info", array(), "any", false, true), "license", array(), "any", true, true)) {
                        // line 229
                        echo "                            <div class=\"plugin-license\">
                                ";
                        // line 230
                        if ($this->getAttribute($this->getAttribute($context["plugin"], "info", array(), "any", false, true), "license_file", array(), "any", true, true)) {
                            echo "<a title=\"";
                            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_LicenseHomepage")), "html", null, true);
                            echo "\" rel=\"noreferrer noopener\" target=\"_blank\" href=\"index.php?module=CorePluginsAdmin&action=showLicense&pluginName=";
                            echo \Piwik\piwik_escape_filter($this->env, $context["name"], "html", null, true);
                            echo "\">";
                        }
                        echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($this->getAttribute($context["plugin"], "info", array()), "license", array()), "html", null, true);
                        if ($this->getAttribute($this->getAttribute($context["plugin"], "info", array(), "any", false, true), "license_file", array(), "any", true, true)) {
                            echo "</a>";
                        }
                        // line 231
                        echo "                            </div>
                        ";
                    }
                    // line 233
                    echo "                        ";
                    if ($this->getAttribute($this->getAttribute($context["plugin"], "info", array(), "any", false, true), "authors", array(), "any", true, true)) {
                        // line 234
                        echo "                            <div class=\"plugin-author\">
                                By
                                    ";
                        // line 236
                        if ($this->getAttribute($this->getAttribute($context["plugin"], "info", array(), "any", false, true), "authors", array(), "any", true, true)) {
                            // line 237
                            ob_start();
                            // line 238
                            echo "                                        ";
                            $context['_parent'] = $context;
                            $context['_seq'] = twig_ensure_traversable($this->getAttribute($this->getAttribute($context["plugin"], "info", array()), "authors", array()));
                            $context['loop'] = array(
                              'parent' => $context['_parent'],
                              'index0' => 0,
                              'index'  => 1,
                              'first'  => true,
                            );
                            foreach ($context['_seq'] as $context["_key"] => $context["author"]) {
                                if ($this->getAttribute($context["author"], "name", array())) {
                                    // line 239
                                    echo "                                            ";
                                    if ($this->getAttribute($context["author"], "homepage", array(), "any", true, true)) {
                                        // line 240
                                        echo "                                                <a title=\"";
                                        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_AuthorHomepage")), "html", null, true);
                                        echo "\" href=\"";
                                        echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["author"], "homepage", array()), "html", null, true);
                                        echo "\" rel=\"noreferrer noopener\" target=\"_blank\">";
                                        echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["author"], "name", array()), "html", null, true);
                                        echo "</a>
                                            ";
                                    } else {
                                        // line 242
                                        echo "                                                ";
                                        echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["author"], "name", array()), "html", null, true);
                                        echo "
                                            ";
                                    }
                                    // line 244
                                    echo "                                            ";
                                    if (($this->getAttribute($context["loop"], "index", array()) < twig_length_filter($this->env, $this->getAttribute($this->getAttribute($context["plugin"], "info", array()), "authors", array())))) {
                                        // line 245
                                        echo "                                                ,
                                            ";
                                    }
                                    // line 247
                                    echo "                                        ";
                                    ++$context['loop']['index0'];
                                    ++$context['loop']['index'];
                                    $context['loop']['first'] = false;
                                }
                            }
                            $_parent = $context['_parent'];
                            unset($context['_seq'], $context['_iterated'], $context['_key'], $context['author'], $context['_parent'], $context['loop']);
                            $context = array_intersect_key($context, $_parent) + $_parent;
                            // line 248
                            echo "                                    ";
                            echo trim(preg_replace('/>\s+</', '><', ob_get_clean()));
                        }
                        // line 249
                        echo ".
                            </div>
                        ";
                    }
                    // line 252
                    echo "                    </td>
                    <td class=\"status\" ";
                    // line 253
                    if (($context["isDefaultTheme"] ?? $this->getContext($context, "isDefaultTheme"))) {
                        echo "style=\"border-left-width:0px;\"";
                    }
                    echo ">
                        ";
                    // line 254
                    if ( !($context["isDefaultTheme"] ?? $this->getContext($context, "isDefaultTheme"))) {
                        // line 256
                        if ($this->getAttribute($context["plugin"], "activated", array())) {
                            // line 257
                            echo "                                ";
                            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_Active")), "html", null, true);
                            echo "
                            ";
                        } else {
                            // line 259
                            echo "                                ";
                            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_Inactive")), "html", null, true);
                            echo "
                                ";
                            // line 260
                            if (($this->getAttribute($context["plugin"], "uninstallable", array()) && ($context["displayAdminLinks"] ?? $this->getContext($context, "displayAdminLinks")))) {
                                echo " <br/> - <a data-plugin-name=\"";
                                echo \Piwik\piwik_escape_filter($this->env, $context["name"], "html_attr");
                                echo "\" class=\"uninstall\" href='index.php?module=CorePluginsAdmin&action=uninstall&pluginName=";
                                echo \Piwik\piwik_escape_filter($this->env, $context["name"], "html", null, true);
                                echo "&nonce=";
                                echo \Piwik\piwik_escape_filter($this->env, ($context["uninstallNonce"] ?? $this->getContext($context, "uninstallNonce")), "html", null, true);
                                echo "'>";
                                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_ActionUninstall")), "html", null, true);
                                echo "</a>";
                            }
                            // line 261
                            echo "                            ";
                        }
                    }
                    // line 264
                    echo "                    </td>

                    ";
                    // line 266
                    if (($context["displayAdminLinks"] ?? $this->getContext($context, "displayAdminLinks"))) {
                        // line 267
                        echo "                        <td class=\"togl action-links\" ";
                        if (($context["isDefaultTheme"] ?? $this->getContext($context, "isDefaultTheme"))) {
                            echo "style=\"border-left-width:0px;\"";
                        }
                        echo ">
                            ";
                        // line 268
                        if ( !($context["isDefaultTheme"] ?? $this->getContext($context, "isDefaultTheme"))) {
                            // line 270
                            if (($this->getAttribute($context["plugin"], "invalid", array(), "any", true, true) || $this->getAttribute($context["plugin"], "alwaysActivated", array()))) {
                                // line 271
                                echo "                                    -
                                ";
                            } else {
                                // line 273
                                echo "                                    ";
                                echo $this->getAttribute($this, "pluginActivateDeactivateAction", array(0 => $context["name"], 1 => $this->getAttribute($context["plugin"], "activated", array()), 2 => $this->getAttribute($context["plugin"], "missingRequirements", array()), 3 => ($context["deactivateNonce"] ?? $this->getContext($context, "deactivateNonce")), 4 => ($context["activateNonce"] ?? $this->getContext($context, "activateNonce"))), "method");
                                echo "
                                ";
                            }
                        }
                        // line 277
                        echo "                        </td>
                    ";
                    }
                    // line 279
                    echo "                </tr>
            ";
                }
                // line 281
                echo "        ";
            }
            $_parent = $context['_parent'];
            unset($context['_seq'], $context['_iterated'], $context['name'], $context['plugin'], $context['_parent'], $context['loop']);
            $context = array_intersect_key($context, $_parent) + $_parent;
            // line 282
            echo "        </tbody>
    </table>

    <div class=\"tableActionBar\">
        ";
            // line 286
            if (($context["isTheme"] ?? $this->getContext($context, "isTheme"))) {
                // line 287
                echo "            <a href=\"";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFunction('linkTo')->getCallable(), array(array("action" => "browseThemes", "sort" => ""))), "html", null, true);
                echo "\"><span class=\"icon-add\"></span> ";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_InstallNewThemes")), "html", null, true);
                echo "</a>
        ";
            } else {
                // line 289
                echo "            <a href=\"";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFunction('linkTo')->getCallable(), array(array("action" => "browsePlugins", "sort" => ""))), "html", null, true);
                echo "\"><span class=\"icon-add\"></span> ";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_InstallNewPlugins")), "html", null, true);
                echo "</a>
        ";
            }
            // line 291
            echo "    </div>

    <div class=\"footer-message\">
        ";
            // line 294
            ob_start();
            // line 295
            echo "            ";
            $context['_parent'] = $context;
            $context['_seq'] = twig_ensure_traversable(($context["pluginsInfo"] ?? $this->getContext($context, "pluginsInfo")));
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
            foreach ($context['_seq'] as $context["name"] => $context["plugin"]) {
                // line 296
                echo "                ";
                if (($this->getAttribute($context["plugin"], "alwaysActivated", array(), "any", true, true) && $this->getAttribute($context["plugin"], "alwaysActivated", array()))) {
                    // line 297
                    echo "                    ";
                    echo \Piwik\piwik_escape_filter($this->env, $context["name"], "html", null, true);
                    if ( !$this->getAttribute($context["loop"], "last", array())) {
                        echo ", ";
                    }
                    // line 298
                    echo "                 ";
                }
                // line 299
                echo "            ";
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
            unset($context['_seq'], $context['_iterated'], $context['name'], $context['plugin'], $context['_parent'], $context['loop']);
            $context = array_intersect_key($context, $_parent) + $_parent;
            // line 300
            echo "        ";
            $context["pluginsAlwaysActivated"] = ('' === $tmp = ob_get_clean()) ? '' : new Twig_Markup($tmp, $this->env->getCharset());
            // line 301
            echo "        ";
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("CorePluginsAdmin_AlwaysActivatedPluginsList", ($context["pluginsAlwaysActivated"] ?? $this->getContext($context, "pluginsAlwaysActivated")))), "html", null, true);
            echo "
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
        return "@CorePluginsAdmin/macros.twig";
    }

    public function isTraitable()
    {
        return false;
    }

    public function getDebugInfo()
    {
        return array (  935 => 301,  932 => 300,  918 => 299,  915 => 298,  909 => 297,  906 => 296,  888 => 295,  886 => 294,  881 => 291,  873 => 289,  865 => 287,  863 => 286,  857 => 282,  851 => 281,  847 => 279,  843 => 277,  836 => 273,  832 => 271,  830 => 270,  828 => 268,  821 => 267,  819 => 266,  815 => 264,  811 => 261,  799 => 260,  794 => 259,  788 => 257,  786 => 256,  784 => 254,  778 => 253,  775 => 252,  770 => 249,  766 => 248,  756 => 247,  752 => 245,  749 => 244,  743 => 242,  733 => 240,  730 => 239,  718 => 238,  716 => 237,  714 => 236,  710 => 234,  707 => 233,  703 => 231,  691 => 230,  688 => 229,  686 => 228,  683 => 227,  676 => 223,  673 => 222,  665 => 219,  661 => 217,  658 => 216,  652 => 214,  649 => 213,  641 => 211,  639 => 210,  634 => 208,  630 => 207,  624 => 206,  616 => 205,  613 => 204,  611 => 203,  608 => 202,  600 => 199,  597 => 198,  595 => 192,  590 => 190,  585 => 187,  578 => 184,  576 => 183,  571 => 180,  562 => 178,  559 => 177,  557 => 176,  540 => 174,  534 => 172,  530 => 170,  526 => 169,  523 => 168,  521 => 167,  517 => 166,  493 => 164,  490 => 163,  487 => 162,  483 => 161,  478 => 158,  472 => 156,  470 => 155,  466 => 154,  462 => 153,  454 => 152,  444 => 145,  440 => 144,  436 => 143,  431 => 140,  412 => 139,  392 => 133,  388 => 132,  384 => 131,  380 => 130,  373 => 126,  369 => 125,  365 => 124,  361 => 123,  357 => 122,  352 => 119,  341 => 118,  319 => 114,  315 => 112,  313 => 111,  305 => 110,  303 => 109,  287 => 108,  253 => 88,  235 => 72,  227 => 69,  223 => 67,  215 => 65,  213 => 64,  206 => 63,  204 => 62,  200 => 61,  191 => 60,  189 => 59,  185 => 57,  179 => 55,  173 => 53,  171 => 52,  165 => 49,  161 => 48,  157 => 46,  149 => 44,  142 => 42,  134 => 40,  132 => 39,  125 => 35,  121 => 34,  114 => 30,  106 => 29,  95 => 26,  91 => 25,  84 => 21,  80 => 20,  76 => 19,  72 => 18,  68 => 17,  54 => 6,  50 => 4,  47 => 3,  33 => 2,  28 => 138,  25 => 117,  22 => 107,  19 => 1,);
    }

    /** @deprecated since 1.27 (to be removed in 2.0). Use getSourceContext() instead */
    public function getSource()
    {
        @trigger_error('The '.__METHOD__.' method is deprecated since version 1.27 and will be removed in 2.0. Use getSourceContext() instead.', E_USER_DEPRECATED);

        return $this->getSourceContext()->getCode();
    }

    public function getSourceContext()
    {
        return new Twig_Source("
{% macro tablePluginUpdates(pluginsHavingUpdate, updateNonce, isMultiServerEnvironment) %}
    {% import '@Marketplace/macros.twig' as marketplaceMacro %}

    <div>
        <a id=\"update-selected-plugins\" href=\"javascript:\" class=\"btn\">{{ 'CorePluginsAdmin_UpdateSelected'|translate }}</a>
    </div>
    <table piwik-content-table>
        <thead>
        <tr>
            <th>
                <span class=\"checkbox-container\">
                    <input type=\"checkbox\" id=\"select-plugin-all\" />
                    <label for=\"select-plugin-all\"></label>
                </span>
            </th>
            <th>{{ 'General_Plugin'|translate }}</th>
            <th class=\"num\">{{ 'CorePluginsAdmin_Version'|translate }}</th>
            <th>{{ 'General_Description'|translate }}</th>
            <th class=\"status\">{{ 'CorePluginsAdmin_Status'|translate }}</th>
            <th class=\"action-links\">{{ 'General_Action'|translate }}</th>
        </tr>
        </thead>
        <tbody id=\"plugins\">
        {% for name,plugin in pluginsHavingUpdate %}
            <tr {% if plugin.isActivated|default(false) %}class=\"active-plugin\"{% else %}class=\"inactive-plugin\"{% endif %}>
                <td class=\"select-cell\">
                    <span class=\"checkbox-container\">
                        <input type=\"checkbox\" id=\"select-plugin-{{ plugin.name|e('html_attr') }}\" {% if plugin.isDownloadable is defined and not plugin.isDownloadable %}disabled=\"disabled\"{% endif %} />
                        <label for=\"select-plugin-{{ plugin.name|e('html_attr') }}\"></label>
                    </span>
                </td>
                <td class=\"name\">
                    <a href=\"javascript:void(0);\" piwik-plugin-name=\"{{ plugin.name|e('html_attr') }}\" class=\"plugin-details\">
                        {{ plugin.name }}
                    </a>
                </td>
                <td class=\"vers\">
                    {% if plugin.changelog is defined and plugin.changelog and plugin.changelog.url is defined and plugin.changelog.url %}
                        <a href=\"{{ plugin.changelog.url|e('html_attr') }}\" title=\"{{ 'CorePluginsAdmin_Changelog'|translate }}\"
                           target=\"_blank\" rel=\"noreferrer noopener\"
                        >{{ plugin.currentVersion }} => {{ plugin.latestVersion }}</a>
                    {% else %}
                        {{ plugin.currentVersion }} => {{ plugin.latestVersion }}
                    {% endif %}
                </td>
                <td class=\"desc\">
                    {{ plugin.description }}
                    {{ marketplaceMacro.missingRequirementsPleaseUpdateNotice(plugin) }}
                </td>
                <td class=\"status\">
                    {% if plugin.isActivated %}
                        {{ 'CorePluginsAdmin_Active'|translate }}
                    {% else %}
                        {{ 'CorePluginsAdmin_Inactive'|translate }}
                    {% endif %}
                </td>
                <td class=\"togl action-links\">
                    {% if plugin.isDownloadable is defined and not plugin.isDownloadable %}
                        <span title=\"{{ 'CorePluginsAdmin_PluginNotDownloadable'|translate|e('html_attr') }} {% if plugin.isPaid %}{{ 'CorePluginsAdmin_PluginNotDownloadablePaidReason'|translate|e('html_attr') }}{% endif %}\"
                          >{{ 'CorePluginsAdmin_NotDownloadable'|translate|e('html_attr') }}</span>
                    {% elseif isMultiServerEnvironment %}
                        <a onclick=\"\$(this).css('display', 'none')\" href=\"{{ linkTo({'action':'download', 'module': 'Marketplace', 'pluginName': plugin.name, 'nonce': (plugin.name|nonce)}) }}\">{{ 'General_Download'|translate }}</a>
                    {% elseif 0 == plugin.missingRequirements|length %}
                        <a href=\"{{ linkTo({'action':'updatePlugin', 'module': 'Marketplace', 'pluginName': plugin.name, 'nonce': updateNonce}) }}\">{{ 'CoreUpdater_UpdateTitle'|translate }}</a>
                    {% else %}
                        -
                    {% endif %}
                </td>
            </tr>
        {% endfor %}
        </tbody>
    </table>

    <script>
        \$(function () {
            \$('#update-selected-plugins').on('click', function () {
                var pluginsToUpdate = [];
                \$('tbody#plugins td.select-cell input').each(function () {
                    if (!this.checked) {
                        return;
                    }

                    var pluginName = \$(this).closest('tr').find('.name .plugin-details').attr('piwik-plugin-name');
                    pluginsToUpdate.push(pluginName);
                });

                var url = '{{ linkTo({'module':'Marketplace','action':'updatePlugin','nonce':updateNonce})|raw }}&pluginName=' + encodeURIComponent(pluginsToUpdate.join(','));
                window.location.href = url;

                \$(this).prop('disabled', true);
            });

            \$('#select-plugin-all').on('change', function () {
                var self = this;
                \$('tbody#plugins td.select-cell input[type=checkbox]').each(function () {
                    if (\$(this).prop('disabled')) {
                        return;
                    }
                    \$(this).prop('checked', self.checked);
                });
            });
        });
    </script>

{% endmacro %}

{% macro pluginActivateDeactivateAction(name, isActivated, missingRequirements, deactivateNonce, activateNonce) -%}
    {%- if isActivated -%}
        <a href='index.php?module=CorePluginsAdmin&action=deactivate&pluginName={{ name }}&nonce={{ deactivateNonce }}&redirectTo=referrer'>{{ 'CorePluginsAdmin_Deactivate'|translate }}</a>
    {%- elseif missingRequirements %}
        -
    {% else -%}
        <a href='index.php?module=CorePluginsAdmin&action=activate&pluginName={{ name }}&nonce={{  activateNonce }}&redirectTo=referrer'>{{ 'CorePluginsAdmin_Activate'|translate }}</a>
    {%- endif -%}
{%- endmacro %}

{% macro pluginsFilter() %}

    <p class=\"row pluginsFilter\" piwik-plugin-filter>
        <span class=\"origin\">
            <strong>{{ 'CorePluginsAdmin_Origin'|translate }}</strong>
            <a data-filter-origin=\"all\" href=\"#\" class=\"active\">{{ 'General_All'|translate }}<span class=\"counter\"></span></a> |
            <a data-filter-origin=\"core\" href=\"#\">{{ 'CorePluginsAdmin_OriginCore'|translate }}<span class=\"counter\"></span></a> |
            <a data-filter-origin=\"official\" href=\"#\">{{ 'CorePluginsAdmin_OriginOfficial'|translate }}<span class=\"counter\"></span></a> |
            <a data-filter-origin=\"thirdparty\" href=\"#\">{{ 'CorePluginsAdmin_OriginThirdParty'|translate }}<span class=\"counter\"></span></a>
        </span>

        <span class=\"status\">
            <strong>{{ 'CorePluginsAdmin_Status'|translate }}</strong>
            <a data-filter-status=\"all\" href=\"#\" class=\"active\">{{ 'General_All'|translate }}<span class=\"counter\"></span></a> |
            <a data-filter-status=\"active\" href=\"#\">{{ 'CorePluginsAdmin_Active'|translate }}<span class=\"counter\"></span></a> |
            <a data-filter-status=\"inactive\" href=\"#\">{{ 'CorePluginsAdmin_Inactive'|translate }}<span class=\"counter\"></span></a>
        </span>
    </p>

{% endmacro %}

{% macro tablePlugins(pluginsInfo, pluginNamesHavingSettings, activateNonce, deactivateNonce, uninstallNonce, isTheme, marketplacePluginNames, displayAdminLinks) %}

    <div id=\"confirmUninstallPlugin\" class=\"ui-confirm\">

        <h2 id=\"uninstallPluginConfirm\">{{ 'CorePluginsAdmin_UninstallConfirm'|translate }}</h2>
        <input role=\"yes\" type=\"button\" value=\"{{ 'General_Yes'|translate }}\"/>
        <input role=\"no\" type=\"button\" value=\"{{ 'General_No'|translate }}\"/>

    </div>

    <table piwik-content-table>
        <thead>
        <tr>
            <th>{% if isTheme %}{{ 'CorePluginsAdmin_Theme'|translate }}{% else %}{{ 'General_Plugin'|translate }}{% endif %}</th>
            <th>{{ 'General_Description'|translate }}</th>
            <th class=\"status\">{{ 'CorePluginsAdmin_Status'|translate }}</th>
            {% if (displayAdminLinks) %}
                <th class=\"action-links\">{{ 'General_Action'|translate }}</th>
            {% endif %}
        </tr>
        </thead>
        <tbody id=\"plugins\">
        {% for name,plugin in pluginsInfo %}
            {% set isDefaultTheme = isTheme and name == 'Morpheus' %}
            {% if (plugin.alwaysActivated is defined and not plugin.alwaysActivated) or isTheme %}
                <tr {% if plugin.activated %}class=\"active-plugin\"{% else %}class=\"inactive-plugin\"{% endif %} data-filter-status=\"{% if plugin.activated %}active{% else %}inactive{% endif %}\" data-filter-origin=\"{% if plugin.isCorePlugin %}core{% elseif plugin.isOfficialPlugin %}official{% else %}thirdparty{% endif %}\">
                    <td class=\"name\">
                        <a name=\"{{ name|e('html_attr') }}\"></a>
                        {% if not plugin.isCorePlugin and name in marketplacePluginNames -%}
                            <a href=\"javascript:void(0);\"
                               piwik-plugin-name=\"{{ name|e('html_attr') }}\"
                            >{{ name }}</a>
                        {%- else %}
                            {{ name }}
                        {% endif %}
                        <span class=\"plugin-version\" {% if plugin.isCorePlugin %}title=\"{{ 'CorePluginsAdmin_CorePluginTooltip'|translate }}\"{% endif %}>({% if plugin.isCorePlugin %}{{ 'CorePluginsAdmin_OriginCore'|translate }}{% else %}v{{ plugin.info.version }}{% endif %})</span>

                        {% if name in pluginNamesHavingSettings %}
                            <br /><br />
                            <a href=\"{{ linkTo({'module':'CoreAdminHome', 'action': 'generalSettings'}) }}#{{ name|e('html_attr') }}\" class=\"settingsLink\">{{ 'General_Settings'|translate }}</a>
                        {% endif %}
                    </td>
                    <td class=\"desc\">
                        <div class=\"plugin-desc-missingrequirements\">
                            {% if plugin.missingRequirements is defined and plugin.missingRequirements %}
                                {{  plugin.missingRequirements }}
                                <br />
                            {% endif %}
                        </div>
                        <div class=\"plugin-desc-text\">

                            {{ plugin.info.description|nl2br }}

                            {% if plugin.info.homepage|default is not empty and plugin.info.homepage not in [
                            'http://piwik.org', 'http://www.piwik.org', 'http://piwik.org/', 'http://www.piwik.org/',
                            'https://piwik.org', 'https://www.piwik.org', 'https://piwik.org/', 'https://www.piwik.org/',
                                'http://matomo.org', 'http://www.matomo.org', 'http://matomo.org/', 'http://www.matomo.org/',
                                'https://matomo.org', 'https://www.matomo.org', 'https://matomo.org/', 'https://www.matomo.org/'
                            ] %}
                                <span class=\"plugin-homepage\">
                            <a target=\"_blank\" rel=\"noreferrer noopener\" href=\"{{ plugin.info.homepage|e('html_attr') }}\">({{ 'CorePluginsAdmin_PluginHomepage'|translate|replace({' ': '&nbsp;'})|raw }})</a>
                        </span>
                            {% endif %}

                            {% if plugin.info.donate is defined and plugin.info.donate|length %}
                                <div class=\"plugin-donation\">
                                    {{ 'CorePluginsAdmin_LikeThisPlugin'|translate }} <a href=\"javascript:;\" class=\"plugin-donation-link\" data-overlay-id=\"overlay-{{ name|escape('html_attr') }}\">{{ 'CorePluginsAdmin_ConsiderDonating'|translate }}</a>
                                    <div id=\"overlay-{{ name|escape('html_attr') }}\" class=\"donation-overlay ui-confirm\" title=\"{{ 'CorePluginsAdmin_LikeThisPlugin'|translate }}\">
                                        <p>{{ 'CorePluginsAdmin_CommunityContributedPlugin'|translate }}</p>
                                        <p>{{ 'CorePluginsAdmin_ConsiderDonatingCreatorOf'|translate(\"<b>\" ~ name ~ \"</b>\")|raw }}</p>
                                        <div class=\"donation-links\">
                                            {% if plugin.info.donate.paypal is defined and plugin.info.donate.paypal %}
                                                <a class=\"donation-link paypal\" target=\"_blank\" rel=\"noreferrer noopener\" href=\"https://www.paypal.com/cgi-bin/webscr?cmd=_donations&item_name=Matomo%20Plugin%20{{ name|escape('url') }}&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted&business={{ plugin.info.donate.paypal|escape('url') }}\"><img src=\"plugins/CorePluginsAdmin/images/paypal_donate.png\" height=\"30\"/></a>
                                            {% endif %}
                                            {% if plugin.info.donate.flattr is defined and plugin.info.donate.flattr %}
                                                <a class=\"donation-link flattr\" target=\"_blank\" rel=\"noreferrer noopener\" href=\"{{ plugin.info.donate.flattr }}\"><img class=\"alignnone\" title=\"Flattr\" alt=\"\" src=\"plugins/CorePluginsAdmin/images/flattr.png\" height=\"29\" /></a>
                                            {% endif %}
                                            {% if plugin.info.donate.bitcoin is defined and plugin.info.donate.bitcoin %}
                                                <div class=\"donation-link bitcoin\">
                                                    <span>Donate Bitcoins to:</span>
                                                    <a href=\"bitcoin:{{ plugin.info.donate.bitcoin|escape('url') }}\">{{ plugin.info.donate.bitcoin }}</a>
                                                </div>
                                            {% endif %}
                                        </div>
                                        <input role=\"no\" type=\"button\" value=\"{{ 'General_Close'|translate }}\"/>
                                    </div>
                                </div>
                            {% endif %}
                        </div>
                        {% if plugin.info.license is defined %}
                            <div class=\"plugin-license\">
                                {% if plugin.info.license_file is defined %}<a title=\"{{ 'CorePluginsAdmin_LicenseHomepage'|translate }}\" rel=\"noreferrer noopener\" target=\"_blank\" href=\"index.php?module=CorePluginsAdmin&action=showLicense&pluginName={{ name }}\">{% endif %}{{ plugin.info.license }}{% if plugin.info.license_file is defined %}</a>{% endif %}
                            </div>
                        {% endif %}
                        {% if plugin.info.authors is defined %}
                            <div class=\"plugin-author\">
                                By
                                    {% if plugin.info.authors is defined -%}
                                    {% spaceless %}
                                        {% for author in plugin.info.authors if author.name %}
                                            {% if author.homepage is defined %}
                                                <a title=\"{{ 'CorePluginsAdmin_AuthorHomepage'|translate }}\" href=\"{{ author.homepage }}\" rel=\"noreferrer noopener\" target=\"_blank\">{{ author.name }}</a>
                                            {% else %}
                                                {{ author.name }}
                                            {% endif %}
                                            {% if loop.index < plugin.info.authors|length %}
                                                ,
                                            {% endif %}
                                        {% endfor %}
                                    {% endspaceless %}
                                    {%- endif %}.
                            </div>
                        {% endif %}
                    </td>
                    <td class=\"status\" {% if isDefaultTheme %}style=\"border-left-width:0px;\"{% endif %}>
                        {% if not isDefaultTheme -%}

                            {% if plugin.activated %}
                                {{ 'CorePluginsAdmin_Active'|translate }}
                            {% else %}
                                {{ 'CorePluginsAdmin_Inactive'|translate }}
                                {% if plugin.uninstallable and displayAdminLinks %} <br/> - <a data-plugin-name=\"{{ name|escape('html_attr') }}\" class=\"uninstall\" href='index.php?module=CorePluginsAdmin&action=uninstall&pluginName={{ name }}&nonce={{ uninstallNonce }}'>{{ 'CorePluginsAdmin_ActionUninstall'|translate }}</a>{% endif %}
                            {% endif %}

                        {%- endif %}
                    </td>

                    {% if displayAdminLinks %}
                        <td class=\"togl action-links\" {% if isDefaultTheme %}style=\"border-left-width:0px;\"{% endif %}>
                            {% if not isDefaultTheme -%}

                                {% if plugin.invalid is defined or plugin.alwaysActivated %}
                                    -
                                {% else %}
                                    {{ _self.pluginActivateDeactivateAction(name, plugin.activated, plugin.missingRequirements, deactivateNonce, activateNonce) }}
                                {% endif %}

                            {%- endif %}
                        </td>
                    {% endif %}
                </tr>
            {% endif %}
        {% endfor %}
        </tbody>
    </table>

    <div class=\"tableActionBar\">
        {% if isTheme %}
            <a href=\"{{ linkTo({'action':'browseThemes', 'sort': ''}) }}\"><span class=\"icon-add\"></span> {{ 'CorePluginsAdmin_InstallNewThemes'|translate }}</a>
        {% else %}
            <a href=\"{{ linkTo({'action':'browsePlugins', 'sort': ''}) }}\"><span class=\"icon-add\"></span> {{ 'CorePluginsAdmin_InstallNewPlugins'|translate }}</a>
        {% endif %}
    </div>

    <div class=\"footer-message\">
        {% set pluginsAlwaysActivated %}
            {% for name,plugin in pluginsInfo %}
                {% if plugin.alwaysActivated is defined and plugin.alwaysActivated %}
                    {{ name }}{% if not loop.last %}, {% endif %}
                 {% endif %}
            {% endfor %}
        {% endset %}
        {{ 'CorePluginsAdmin_AlwaysActivatedPluginsList'|translate(pluginsAlwaysActivated) }}
    </div>

{% endmacro %}
", "@CorePluginsAdmin/macros.twig", "D:\\xampp\\htdocs\\matomo\\plugins\\CorePluginsAdmin\\templates\\macros.twig");
    }
}
