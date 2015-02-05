<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<!-- Copyright © 2005-2006 Jonathan Porter [http://j.porter.name/]

This work is licensed under the Creative Commons Attribution-ShareAlike
License. To view a copy of this license, visit
http://creativecommons.org/licenses/by-sa/2.0/ or send a letter to Creative
Commons, 559 Nathan Abbott Way, Stanford, California 94305, USA.

This is an XSL Transform (XSLT) of XHTML 1.0 (Strict) to HTML 4.01 (Strict).
Additionally, this includes transformation of <?xml-stylesheet?> processing
instructions as described in 'Associating Style Sheets with XML documents
Version 1.0' [http://www.w3.org/1999/06/REC-xml-stylesheet-19990629/] to their
HTML equivalent.

NOTE: This is a version 2.0 XSL Transform. It is backwards-compatible with
version 1.0 XSLT processors, however, version 1.0 XSLT processors cannot
transform <?xml-stylesheet?> processing instructions.

'XHTML' refers to the strict versions of 'XHTML™ 1.0 The Extensible HyperText
Markup Language (Second Edition)'
[http://www.w3.org/TR/2002/REC-xhtml1-20020801/] and 'XHTML™ 1.0 in XML Schema'
[http://www.w3.org/TR/2002/NOTE-xhtml1-schema-20020902].  'HTML' refers to the
strict version in the 'HTML 4.01 Specification'
[http://www.w3.org/TR/1999/REC-html401-19991224].  The declarations for XHTML
and HTML apply in all instances unless otherwise specified. -->
<xsl:stylesheet 
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:xhtml="http://www.w3.org/1999/xhtml"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  exclude-result-prefixes="xs xhtml xsi"
  version="2.0">
  
  <xsl:output method="html"
              byte-order-mark="no"
              doctype-public="-//W3C//DTD HTML 4.01//EN"
              doctype-system="http://www.w3.org/TR/html4/strict.dtd"
              encoding="ISO-8859-1"
              include-content-type="no"
              indent="no"
              media-type="text/html"
              omit-xml-declaration="yes"
              version="4.01"/>

  <xsl:preserve-space elements="xhtml:style xhtml:script xhtml:pre"/>

  <xsl:variable name="enable-stylesheet-pi-processing" select="(number(system-property('xsl:version')) >= 2.0) and /processing-instruction('xml-stylesheet')"/>

  <xsl:template match="/processing-instruction('xml-stylesheet')"/>

  <xsl:template match="xhtml:*">
    <xsl:element name="{translate(local-name(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')}">
      <xsl:choose>
        <xsl:when test="$enable-stylesheet-pi-processing and local-name() = 'head'">
          <xsl:call-template name="StyleStylesheetPIsTOLinksInHead"/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:apply-templates/>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:element>
  </xsl:template>

  <xsl:template match="@xml:lang">
    <xsl:attribute name="lang">
      <xsl:value-of select="."/>
    </xsl:attribute>
  </xsl:template>

  <xsl:template match="@lang">
    <xsl:if test="not(../@xml:lang)">
      <xsl:attribute name="lang">
        <xsl:value-of select="."/>
      </xsl:attribute>
    </xsl:if>
  </xsl:template>

  <xsl:template match="xhtml:map/@id">
    <xsl:attribute name="id">
      <xsl:value-of select="."/>
    </xsl:attribute>
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
  
  If "name" is specified alone or "id" and "name" attributes are both specified
  but differ the "name" attribute is dropped.  This is done to ensure a valid
  HTML document is produced.  Semantically your XHTML and HTML will be
  equivalent, however, external resources, not defined in the XHTML or HTML
  specifications (e.g. stylesheets, scripts), that rely on the presence of
  "name" attributes may be affected.
  
  NOTE: The rule above only applies to "name" attributes present on the
  elements below, where the "name" attribute is a fragment identifier in HTML.
  It does not apply to other elements that have a "name" attribute such as
  <input> and <meta> elements.  The rule is slightly different for the "name"
  attribute on <map> elements which is also a fragment identifier, but not
  specified below.  The behavior for the "name" attribute on <map> elements is
  described above. -->
  <xsl:template match="xhtml:a/@name|xhtml:form/@name|xhtml:iframe/@name">
    <xsl:if test="../@name = ../@id">
      <xsl:attribute name="name">
        <xsl:value-of select="."/>
      </xsl:attribute>
    </xsl:if>
  </xsl:template>

  <!-- The "name" attribute of the <map> element is ignored for output here.
  See above for additional information on the <map> element's "name" attribute.
  -->
  <xsl:template match="@xml:space|xhtml:html/@version|xhtml:html/@xsi:schemaLocation|xhtml:map/@name"/>

  <xsl:template match="@*|node()">
    <xsl:copy/>
  </xsl:template>

  <xsl:template name="StyleStylesheetPIsTOLinksInHead">
    <xsl:apply-templates select="@*"/>
    <xsl:choose>
      <xsl:when test="xhtml:base">
        <xsl:for-each select="preceding-sibling::xhtml:base">
          <xsl:if test="not(xhtml:style or xhtml:link)">
          <xsl:apply-templates select="node()"/>
          </xsl:if>
        </xsl:for-each>
        <xsl:apply-templates select="xhtml:base"/>
        <xsl:for-each select="/processing-instruction('xml-stylesheet')">
          <xsl:call-template name="StylesheetPIToLink"/>
        </xsl:for-each>
        <xsl:for-each select="preceding-sibling::xhtml:base">
          <xsl:if test="xhtml:style or xhtml:link">
            <xsl:apply-templates select="node()"/>
          </xsl:if>
        </xsl:for-each>
        <xsl:for-each select="preceding-sibling::xhtml:base">
          <xsl:if test="not(xhtml:style or xhtml:link)">
            <xsl:apply-templates select="node()"/>
          </xsl:if>
        </xsl:for-each>
      </xsl:when>
      <xsl:otherwise>
        <xsl:for-each select="/processing-instruction('xml-stylesheet')">
          <xsl:call-template name="StylesheetPIToLink"/>
        </xsl:for-each>
        <xsl:apply-templates select="node()"/>
      </xsl:otherwise>
    </xsl:choose>
    
    <!-- The <base> element MUST proceed any element that refers to an external
    source.  Because we are adding link elements that may refer to external
    sources we are ensuring the <base> element, if present, precedes them. -->
    <xsl:apply-templates select="xhtml:base"/>
    <!-- To ensure proper cascading new <link> elements added to the output
    from <?xml-stylesheet?> processing instructions should precede other
    references to stylesheets.  This includes <link> elements that refer to
    stylesheets and <style> elements.  For this reason we place the new <link>
    elements as the first children of the head element unless a <base> element
    is present in which case the <base> element comes first and the <link>
    elements are placed right after. -->
    
    <xsl:for-each select=""
    
  </xsl:template>

  <xsl:template name="StylesheetPIToLink">
    <xsl:element name="LINK">
      <xsl:attribute name="rel">stylesheet</xsl:attribute>
      <xsl:analyze-string select="."
        regex="(\i(\c)*)(\s)*=(\s)*(&quot;|')(([^&lt;&amp;]|&amp;(#[0-9]+|#x[0-9a-fA-F]+|amp|lt|gt|quot|apos);)*?)\5">
        <xsl:matching-substring>
          <xsl:variable name="attrib-value">
            <xsl:analyze-string select="regex-group(6)" regex="&amp;(#([0-9]+|x([0-9a-fA-F]+))|amp|lt|gt|quot|apos);">
              <xsl:matching-substring>
                <xsl:choose>
                  <xsl:when test="starts-with(regex-group(1), '#')">
                    <xsl:variable name="codepoint">
                      <xsl:choose>
                        <xsl:when test="starts-with(regex-group(2), 'x')">
                          <xsl:call-template name="hexadecimal-to-decimal">
                            <xsl:with-param name="hexadecimal" select="regex-group(3)"/>
                          </xsl:call-template>
                        </xsl:when>
                        <xsl:otherwise>
                          <xsl:value-of select="regex-group(2)"/>
                        </xsl:otherwise>
                      </xsl:choose>
                    </xsl:variable>
                    <xsl:value-of select="codepoints-to-string(xs:integer($codepoint))"/>
                  </xsl:when>
                  <xsl:when test="regex-group(1)='amp'">&amp;</xsl:when>
                  <xsl:when test="regex-group(1)='lt'">&lt;</xsl:when>
                  <xsl:when test="regex-group(1)='gt'">></xsl:when>
                  <xsl:when test="regex-group(1)='quot'">"</xsl:when>
                  <xsl:when test="regex-group(1)='apos'">'</xsl:when>
                </xsl:choose>
              </xsl:matching-substring>
              <xsl:non-matching-substring>
                <xsl:value-of select="."/>
              </xsl:non-matching-substring>
            </xsl:analyze-string>
          </xsl:variable>
          <xsl:choose>
            <xsl:when test="regex-group(1)='alternate'">
              <xsl:if test="normalize-space($attrib-value)='yes'">
                <xsl:attribute name="rel">stylesheet alternate</xsl:attribute>
              </xsl:if>
            </xsl:when>
            <xsl:otherwise>
              <xsl:attribute name="{regex-group(1)}">
                <xsl:choose>
                  <xsl:when test="regex-group(1)='href'">
                    <!-- If the "href" pseudo attribute of the
                    <?xml-stylesheet?> processing instruction is a relative URI
                    reference then it should be relative to the document's URI.
                    For this reason we use the 'resolve-uri' function to ensure
                    that relative references are converted to URIs based on the
                    document's URI and the URI Reference of the "href" pseudo
                    attribute.  This is done so that the <link> elements we
                    create from the <?xml-stylesheet?> processing instructions
                    are not influenced by the <base> element. -->
                    <xsl:value-of select="resolve-uri(normalize-space($attrib-value))"/>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="$attrib-value"/>
                  </xsl:otherwise>
                </xsl:choose>  
              </xsl:attribute>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:matching-substring>
      </xsl:analyze-string>
    </xsl:element>
  </xsl:template>

  <xsl:template name="hexadecimal-to-decimal" as="xs:integer">
    <xsl:param name="hexadecimal" required="yes"/>
    <xsl:param name="running-total" required="no">0</xsl:param>
    <xsl:variable name="char-value"
				  select="string-length(substring-before('0123456789ABCDEF', upper-case(substring($hexadecimal, 1, 1))))"/>
    <xsl:variable name="total" select="$running-total * 16 + $char-value"/>
    <xsl:choose>
      <xsl:when test="string-length($hexadecimal) = 1">
        <xsl:value-of select="$total"/>
      </xsl:when>
      <xsl:otherwise>
        <xsl:call-template name="hexadecimal-to-decimal">
          <xsl:with-param name="hexadecimal" select="substring($hexadecimal, 2)"/>
          <xsl:with-param name="running-total" select="$total"/>
        </xsl:call-template>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

</xsl:stylesheet>