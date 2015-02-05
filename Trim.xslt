<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:param name="max-len" select="15"/>

  <xsl:template match="/">
    <xsl:call-template name="trim"/>
  </xsl:template>

  <xsl:template name="trim">
    <xsl:param name="rlen" select="0"/>
    <xsl:param name="nodes" select="*"/>

    <xsl:choose>
      <xsl:when test="$rlen + string-length($nodes[1]) &lt;= $max-len">
        <xsl:copy-of select="$nodes[1]"/>
        <xsl:if test="$nodes[2]">
          <xsl:call-template name="trim">
            <xsl:with-param name="rlen" select="$rlen + string-length($nodes[1]) "/>
            <xsl:with-param name="nodes" select="$nodes[position() != 1]|$nodes[1]/*"/>
          </xsl:call-template>
        </xsl:if>
      </xsl:when>
      <xsl:when test="$nodes[1]/self::text()">
        <xsl:value-of select="substring($nodes[1], 1, $max-len - $rlen)"/>
        <xsl:text>…</xsl:text>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="$nodes[1]/node()">
          <xsl:element name="{name($nodes[1])}"
                       namespace="{namespace-uri($nodes[1])}">
            <xsl:copy-of select="$nodes[1]/@*"/>
            <xsl:call-template name="trim">
              <xsl:with-param name="rlen" select="$rlen"/>
              <xsl:with-param name="nodes" select="$nodes[1]/node()"/>
            </xsl:call-template>
          </xsl:element>
        </xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
</xsl:stylesheet>
