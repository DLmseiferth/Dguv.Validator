﻿namespace Dguv.Validator
{
    /// <summary>
    /// Schnittstelle für ein Prüfverfahren für einen Unfallversicherungsträger
    /// </summary>
    public interface IDguvNumberCheck
    {
        /// <summary>
        /// Holt die Betriebsnummer des Unfallversicherungsträgers
        /// </summary>
        string BbnrUv { get; }

        /// <summary>
        /// Holt den Namen des Unfallversicherungsträgers
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Prüft, ob die Mitgliedsnummer für den Unfallversicherungsträger gültig ist.
        /// </summary>
        /// <param name="memberId">Die zu prüfenden Mitgliedsnummer</param>
        /// <returns>true, wenn die Mitgliedsnummer gültig ist</returns>
        bool IsValid(string memberId);

        /// <summary>
        /// Prüft, ob die Mitgliedsnummer für den Unfallversicherungsträger gültig ist und liefert im
        /// Falle eines Fehlers die Fehlermeldung zurück.
        /// </summary>
        /// <param name="memberId">Die zu prüfenden Mitgliedsnummer</param>
        /// <returns>null, wenn die Mitgliedsnummer gültig ist, ansonsten die Fehlermeldung</returns>
        string GetStatus(string memberId);
    }
}
