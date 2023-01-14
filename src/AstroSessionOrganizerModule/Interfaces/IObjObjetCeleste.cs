using ApplicationTools;
using System.Collections.Generic;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Interface Objet céleste
    /// </summary>
    public interface IObjObjetCeleste
    {
        /// <summary>
        /// Catalogues
        /// </summary>
        List<IObjCatalogue> Catalogues { get; }

        /// <summary>
        /// Liste des Catalogues renovyés sous la forme d'une chaîne formatée
        /// </summary>
        string CataloguesFormated { get; }

        /// <summary>
        /// Constellation
        /// </summary>
        IObjConstellation Constellation { get; }

        /// <summary>
        /// DEC
        /// </summary>
        Coordinate DEC { get; set; }

        /// <summary>
        /// Liste complète des Catalogues
        /// <para>chaîne de caractère séparés par des ';'</para>
        /// </summary>
        string CompleteCatalogues { get; set; }

        /// <summary>
        /// Liste complète des Dénominations
        /// <para>chaîne de caractère séparés par des ';'</para>
        /// </summary>
        string CompleteDenominations { get; set; }

        /// <summary>
        /// Identifiant de la Constellation
        /// </summary>
        string IdConstellation { get; set; }

        /// <summary>
        /// Identifiant du Type d'Objet céleste
        /// </summary>
        string IdTypeObjet { get; set; }

        /// <summary>
        /// Denominations
        /// </summary>
        List<string> Denominations { get; }

        /// <summary>
        /// Liste des dénominations renovyées sous la forme d'une chaîne formatée
        /// </summary>
        string DenominationsFormated { get; }

        /// <summary>
        /// Description
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// DISTANCE_M
        /// <para>Distance métrique</para>
        /// </summary>
        double? DISTANCE_M { get; set; }

        /// <summary>
        /// DISTANCE_RS
        /// <para>Distance Redshift</para>
        /// </summary>
        double? DISTANCE_RS { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// MAG_PHOTO
        /// <para>Magnitude photo (BMag)</para>
        /// </summary>
        double? MAG_PHOTO { get; set; }

        /// <summary>
        /// MAG_VISUAL
        /// <para>Magnitude visuelle</para>
        /// </summary>
        double? MAG_VISUAL { get; set; }

        /// <summary>
        /// Nom
        /// </summary>
        string Nom { get; set; }

        /// <summary>
        /// RA
        /// </summary>
        Coordinate RA { get; set; }

        /// <summary>
        /// REDSHIFT
        /// </summary>
        double? REDSHIFT { get; set; }

        /// <summary>
        /// SIZE_MAX
        /// </summary>
        double? SIZE_MAX { get; set; }

        /// <summary>
        /// Grandeur Max renvoyée sous la forme d'une chaîne formaté
        /// </summary>
        string GrandeurMaxFormated { get; }

        /// <summary>
        /// SIZE_MIN
        /// </summary>
        double? SIZE_MIN { get; set; }

        /// <summary>
        /// Grandeur Min renvoyée sous la forme d'une chaîne formaté
        /// </summary>
        string GrandeurMinFormated { get; }

        /// <summary>
        /// Thumbnail
        /// </summary>
        string Thumbnail { get; set; }

        /// <summary>
        /// ThumbnailPosition
        /// </summary>
        string ThumbnailPosition { get; set; }

        /// <summary>
        /// TypeObjet
        /// </summary>
        IObjTypeObjet TypeObjet { get; }

        /// <summary>
        /// URLWiki
        /// </summary>
        string URLWiki { get; set; }

        /// <summary>
        /// URLWIconeiki
        /// </summary>
        string Icone { get; }

        /// <summary>
        /// IconeIndex
        /// </summary>
        int IconeIndex { get; }

        /// <summary>
        /// Flag permettant de savoir si l'objet fait partie du catalogue d'origine
        /// <para>0/1</para>
        /// </summary>
        string Origin { get; set; }

        /// <summary>
        /// Commentaires
        /// </summary>
        string Comment { get; set; }

        /// <summary>
        /// Flag permettant de savoir si l'objet fait partie du catalogue d'origine
        /// <para>0/1</para>
        /// </summary>
        bool CatalogueInitial { get; }
    }
}