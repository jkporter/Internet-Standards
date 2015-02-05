<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output version="2.0" method="text" media-type="application/x-www-form-urlencoded" indent="no" />

  <xsl:template match="/">
    <xsl:for-each select="//*[count(*) = 0]"><xsl:if test="position() > 1"><xsl:text>&amp;</xsl:text></xsl:if><xsl:value-of select="encode-for-uri(local-name())"/>=<xsl:value-of select="encode-for-uri(.)"/></xsl:for-each>
  </xsl:template>
  
  <xsl:template match="text()"/>
</xsl:stylesheet>