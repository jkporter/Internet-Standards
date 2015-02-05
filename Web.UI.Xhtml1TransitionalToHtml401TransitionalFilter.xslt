<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<!-- Copyright © 2008 Jonathan Porter [http://j.porter.name/]

This work is licensed under the Creative Commons Attribution-ShareAlike
License. To view a copy of this license, visit
http://creativecommons.org/licenses/by-sa/2.0/ or send a letter to Creative
Commons, 559 Nathan Abbott Way, Stanford, California 94305, USA.

This is an XSL Transform (XSLT) of XHTML 1.0 (Transitional) to HTML 4.01
(Transitional).

NOTE: This is a version 2.0 XSL Transform. It is backwards-compatible with
version 1.0 XSLT processors.

'XHTML' refers to the transitional versions of 'XHTML™ 1.0 The Extensible
HyperText Markup Language (Second Edition)' 
[http://www.w3.org/TR/2002/REC-xhtml1-20020801/] or 'XHTML™ 1.0 in XML Schema'
[http://www.w3.org/TR/2002/NOTE-xhtml1-schema-20020902] and in accordance with
appendix C.8 [http://www.w3.org/TR/2002/REC-xhtml1-20020801/#C_8] of the former
specification.

'HTML' refers to the  transitional version in the 'HTML 4.01 Specification'
[http://www.w3.org/TR/1999/REC-html401-19991224].

The declarations for XHTML and HTML apply in all instances unless otherwise
specified. -->
<xsl:stylesheet 
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xhtml="http://www.w3.org/1999/xhtml"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  exclude-result-prefixes="xhtml xsi"
  version="2.0">
  
  <xsl:output method="html"
              byte-order-mark="no"
              doctype-public="-//W3C//DTD HTML 4.01 Transitional//EN"
              doctype-system="http://www.w3.org/TR/html4/loose.dtd"
              encoding="UTF-8"
              include-content-type="no"
              indent="no"
              media-type="text/html"
              omit-xml-declaration="yes"
              version="4.01"/>

  <xsl:preserve-space elements="xhtml:style xhtml:script xhtml:pre"/>

  <xsl:template match="@*|node()" name="default">
    <xsl:copy/>
  </xsl:template>
  
  <xsl:template match="xhtml:*">
    <xsl:element name="{translate(local-name(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')}">
      <xsl:apply-templates select="@*|node()"/>
    </xsl:element>
  </xsl:template>

  <xsl:template match="@xml:lang">
    <xsl:attribute name="lang">
      <xsl:value-of select="."/>
    </xsl:attribute>
  </xsl:template>

  <xsl:template match="@lang">
    <xsl:if test="not(../@xml:lang)">
      <xsl:call-template name="default"/>
    </xsl:if>
  </xsl:template>

  <xsl:template match="xhtml:map/@id">
    <xsl:call-template name="default"/>
    <!-- "name" attribute is REQUIRED in HTML for <map> elements. A "name"
    attribute on the <map> element, a fragment identifier in HTML, will always 
    be added to the HTML output. The value of the "name" attribute will be the
    same as the REQUIRED "id" attribute of the XHTML source.  See below for
    additional information on the "name" attribute. -->
    <xsl:attribute name="name">
      <xsl:value-of select="."/>
    </xsl:attribute>
  </xsl:template>

  <!-- "name" and "id" attributes in XHTML vs. HTML
  
  The following "name" attributes (and <map> "name" attribute) have lesser
  meaning in XHTML.  They refer to fragment identifiers in HTML but not in
  XHTML and are formally depreciated in XHTML.  When "id" and "name" attributes
  are both specified on the SAME element they MUST be the identical in HTML.
  Additionally, all "name" and "id" attributes MUST be unique between elements
  in HTML.
  
  Neither of these constraints apply to XHTML. Only "id" attributes have to
  be unique. To alleviate any potential confusion do not use a "name" attribute
  without a corresponding "id" attribute with the same value in XHTML you
  intend to be transformable into HTML.  You can and are encouraged to use "id"
  attributes by themselves however.
  
  IMPORTANT:
  
  If a "name" attribute is specified alone or "id" and "name" attributes are
  both specified but differ the "name" attribute is dropped.  This is done to
  ensure a valid HTML document is produced.  Semantically your XHTML and HTML
  will be equivalent, however, external resources, not defined in the XHTML or
  HTML specifications (e.g. stylesheets, scripts), that rely on the presence of
  "name" attributes may be affected.
  
  NOTE: The rule above only applies to "name" attributes present on the
  elements below, where the "name" attribute is a fragment identifier in HTML.
  It does not apply to other elements that have a "name" attribute such as
  <input> and <meta> elements.  The rule is slightly different for the "name"
  attributes on <map> elements which are also fragment identifiers, but not
  specified below.  The behavior for the "name" attribute on <map> elements is
  described above. -->
  <xsl:template match="xhtml:a/@name|xhtml:applet/@name|xhtml:form/@name|xhtml:frame/@name|xhtml:iframe/@name|xhtml:img/@name">
    <xsl:if test="../@name = ../@id">
      <xsl:call-template name="default"/>
    </xsl:if>
  </xsl:template>

  <!-- The "name" attribute of the <map> element is ignored for output here.
  See above for additional information on the <map> element's "name" attribute.
  -->
  <xsl:template match="@xml:space|xhtml:html/@version|xhtml:html/@xsi:schemaLocation|xhtml:map/@name"/>

</xsl:stylesheet>