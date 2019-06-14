// the xquery-hint is
//   Copyright (C) 2013 by Angelo ZERR <angelo.zerr@gmail.com>
// released under the MIT license (../../LICENSE) like the rest of CodeMirror
(function() {
  var Pos = CodeMirror.Pos;
  var XQuery = CodeMirror.XQuery;

  // --------------- token utils ---------------------

  function getToken(editor, pos) {
    return editor.getTokenAt(pos);
  }

  function getPreviousToken(editor, cur, token) {
    return getToken(editor, Pos(cur.line, token.start));
  }

  // --------------- string utils ---------------------

  function startsWithString(str, token) {
    return str.slice(0, token.length).toUpperCase() == token.toUpperCase();
  }

  function trim(str) {
    if (str.trim) {
      return str.trim();
    }
    return str.replace(/^\s+|\s+$/g, '');
  }

  function getStartsWith(cur, token, startIndex) {
    var length = cur.ch - token.start;
    if (!startIndex)
      startIndex = 0;
    var startsWith = token.string.substring(startIndex, length);
    return trim(startsWith);
  }

 

  // --------------- completion utils ---------------------

  function getCompletions(completions, cur, token, options, showHint) {      
    var sortedCompletions = completions.sort(function(a, b) {
      var s1 = a.text;// getKeyWord(a);
      var s2 = b.text;// getKeyWord(b);
      var nameA = s1.toLowerCase(), nameB = s2.toLowerCase()
      if (nameA < nameB) // sort string ascending
        return -1
      if (nameA > nameB)
        return 1
      return 0 // default return value (no sorting)
    });
    var data = {
      list : sortedCompletions,
      from : Pos(cur.line, token.start),
      to : Pos(cur.line, token.end)
    };
    if (CodeMirror.attachContextInfo) {
      // if context info is available, attach it
      CodeMirror.attachContextInfo(data);
    }
    if (options && options.async) {
      showHint(data);
    } else {
      return data;
    }
  }

  CodeMirror.xqueryHint = function(editor, showHint, options) {
    if (showHint instanceof Function) {
      return internalXQueryHint(editor, options, showHint);
    }
    return internalXQueryHint(editor, showHint, options);
  }

  function internalXQueryHint(editor, options, showHint) {
    var completions = [];
    // Find the token at the cursor
    var cur = editor.getCursor(), token = getToken(editor, cur), tprop = token;
    switch (tprop.type) {
    case "keyword":
      var s = getStartsWith(cur, token);
      // templates
      if (CodeMirror.templatesHint) {
        CodeMirror.templatesHint.getCompletions(editor, completions, s);
      }
      break;
    case "string":
      // completion started inside a string, test if it's import/declaration of
      // module
      if (tprop.state.tokenModuleParsing) {
        var s = getStartsWith(cur, token, 1);
        var quote = token.string.charAt(0);
        //populateModuleNamespaces(s, quote, completions, editor, options);
      }
      break;
    case "variable def":
    case "variable":
    case null:
      // do the completion about variable, declared functions and modules.

      // completion should be ignored for parameters function declaration :
      // check if the current token is not inside () of a declared function.
      var functionDecl = token.state.functionDecl;
      if (functionDecl && functionDecl.paramsParsing == true) {
        return getCompletions(completions, cur, token, options, showHint);
      }
      // completion should be ignored for variable declaration : check if there
      // are not "let", "for" or "variable" keyword before the current token
      var previous = getPreviousToken(editor, cur, tprop);
      if (previous) {
        previous = getPreviousToken(editor, cur, previous);
        if (previous
            && previous.type == "keyword"
            && (previous.string == "let" || previous.string == "variable"
                || previous.string == "for" || previous.string == "function"))
          // ignore completion
          return getCompletions(completions, cur, token, options, showHint);
      }

      // show let, declared variables.
      var s = null;
      if (previous && previous.type == "keyword" && previous.string == "if"
          && token.string == "(") {
        // in the case if(, the search string should be empty.
        s = "";
      } else {
        s = getStartsWith(cur, token);
      }

      // test if s ends with ':'
      var prefix = null;
      var funcName = null;
      var prefixIndex = s.lastIndexOf(':');
      if (prefixIndex != -1) {
        // retrieve the prfix anf function name.
        prefix = s.substring(0, prefixIndex);
        funcName = s.substring(prefixIndex + 1, s.length);
      }

      //if (prefix) {
        // test if it's default prefix
        //var module = XQuery.findModuleByPrefix(prefix, token.state.importedModules);
        //if (module) {
          //populateModuleFunctions(module, prefix, funcName, completions);
        //}
      //}

      // local vars (let, for, ...)
      var vars = token.state.localVars;
      //populateVars(s, vars, completions);

      var context = token.state.context;
      while (context) {
        if (context.keepLocals) {
          vars = context.vars;
          //populateVars(s, vars, completions);
          context = context.prev;
        } else {
          context = null;
        }
      }

      // global vars (declare ...)
      var globalVars = token.state.globalVars;
      //populateVars(s, globalVars, completions);

      // parametres of the function (if token is inside a function)
      if (functionDecl) {
        var vars = functionDecl.params;
        populateVars(s, vars, completions);
      }

      // declared functions
      var declaredFunctions = token.state.declaredFunctions
      //populateDeclaredFunctions(s, declaredFunctions, completions);

      // imported modules
      var importedModules = token.state.importedModules
     // populateImportedModules(s, importedModules, completions);

      // default module
      //populateDefaultModulePrefix(s, completions);

      // populate functions of modules which no needs prefix(ex: fn)
      //populateModuleFunctionsNoNeedsPrefix(s, completions);

      // templates
      if (CodeMirror.templatesHint) {
        CodeMirror.templatesHint.getCompletions(editor, completions, s);
      }
    }
    return getCompletions(completions, cur, token, options, showHint)
  }
  ;
})();
