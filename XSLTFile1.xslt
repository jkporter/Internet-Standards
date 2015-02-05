<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xhtml="http://www.w3.org/1999/xhtml">

  <xsl:output encoding="utf-8" media-type="text/plain" method="text"/>

  <xsl:preserve-space elements="xhtml:pre"/>
  
  <xsl:strip-space elements="xhtml:*"/>

  <xsl:template match="xhtml:head"/>

  <xsl:template match="xhtml:body"><xsl:apply-templates/></xsl:template>

  <xsl:template match="xhtml:p[preceding-sibling::xhtml:p]">&#x2029;<xsl:apply-templates/></xsl:template>

  <xsl:template match="xhtml:br">&#x2028;</xsl:template>

  <xsl:template match="xhtml:img">
    <xsl:value-of select="@alt"/>
  </xsl:template>

  <xsl:template match="xhtml:pre//text()"><xsl:value-of select="."/></xsl:template>

  <xsl:template match="text()"><xsl:value-of select="."/></xsl:template>

  <xsl:template match="xhtml:ul/xhtml:li">• <xsl:apply-templates/>&#x2028;</xsl:template>

  <xsl:template match="xhtml:ol/xhtml:li"><xsl:value-of select="position()"/>. <xsl:apply-templates/>&#x2028;</xsl:template>
  
</xsl:stylesheet> 
