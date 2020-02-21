/************************************************************************
 * AISearch - Small search library written in c#
 * Copyright (C) 2020 Marcin Pietrzykowski (mpietrzykowski@wi.zut.edu.pl)
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISearch {
    public interface IState {
        #region Properties

        /// <summary>
        /// Minimalna gwarantowana wygrana dla gracza maksymalizującego.
        /// </summary>
        double Alpha { get; set; }

        /// <summary>
        /// Maksymalna gwarantowana wygrana dla gracza minimalizującego.
        /// </summary>
        double Beta { get; set; }

        /// <summary>
        /// Zwraca i ustawia potomków danego stanu.
        /// </summary>
        List<IState> Children { get; set; }

        /// <summary>
        /// Zwraca lub ustawia głębokość stanu w drzwie.
        /// </summary>
        double Depth { get; set; }

        /// <summary>
        /// Zwraca f. f = g + h.
        /// </summary>
        double F { get; }

        /// <summary>
        /// Zwraca i ustawia g - koszt osiągnięca wybranego stanu.
        /// </summary>
        double G { get; set; }

        /// <summary>
        /// Zwraca h - heurystyczną wartość dla stanu.
        /// </summary>
        double H { get; }

        /// <summary>
        /// Nazwa statnu jednoznacznie go identyfikująca. 
        /// </summary>
        string ID { get; }

        /// <summary>
        /// Definiuje czy wybrany stan jest dostępnu. W grach jak sudoku wybrany stan może być nie osiągalny.
        /// </summary>
        bool IsAdmissible { get; }

        /// <summary>
        /// Zwraca i ustawia przodka danego stanu.
        /// </summary>
        IState Parent { get; set; }

        /// <summary>
        /// Zwraca lub ustawia zazwę początkowego ruchu pozwalającego na dojście do tego stanu.
        /// </summary>
        string RootMove { get; set; }

        #endregion //end Properties

        #region Methods

        /// <summary>
        /// Oblicza h - heurystyczną wartość dla danego stanu.
        /// </summary>
        /// <returns>Heurystyczna wartość.</returns>
        double ComputeHeuristicGrade();

        #endregion //end Methods  
    }
}
