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
    public abstract class BestFirstSearch {

        private class Comparer : IComparer<IState> {
			public int Compare(IState x, IState y) {
				if (x.H < y.H) {
					return -1;
				}
				else if (x.H > y.H) {
					return 1;
				}
				else {
					return 0;
				}
			}
        }

        #region Protected Fields

        /// <summary>
        /// Zbiór Closed
        /// </summary>
        protected Dictionary<string, IState> closed = null;

        /// <summary>
        /// Stan początkowy.
        /// </summary>
        protected IState initialState = null;

		/// <summary>
		/// Liczba rozwiązań, do odnalezienia przez algorytm
		/// </summary>
		protected int numberOfSolutionsToFind;

		/// <summary>
		/// Zbiór Open
		/// </summary>
		protected SimplePriorityQueue open = null;

        /// <summary>
        /// Lista odnalezionych rozwiązań.
        /// </summary>
        protected List<IState> solutions = null;

        #endregion //end Protected Fields

        #region Properties

        /// <summary>
        /// Zbiór Closed
        /// </summary>
        public IList<IState> Closed {
            get { return this.closed.Values.ToList(); }
        }

        /// <summary>
        /// Zbiór Open
        /// </summary>
        public IList<IState> Open {
            get { return this.open.Items; }
        }

        /// <summary>
        /// Lista odnalezionych rozwiązań.
        /// </summary>
        public IList<IState> Solutions {
            get { return this.solutions; }
        }

        #endregion //end Properties

        #region Constructors

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="initialState">Stan początkowy.</param>
        /// <param name="numberOfSolutionsToFind">Definiuje ile rozwiązań stara się znaleźć algorytm.</param>
        public BestFirstSearch(IState initialState, int numberOfSolutionsToFind = 1) {
            this.closed = new Dictionary<string, IState>();
            this.initialState = initialState;
            this.numberOfSolutionsToFind = numberOfSolutionsToFind;
            this.open = new SimplePriorityQueue(new Comparer());
			this.solutions = new List<IState>();
        }

        #endregion //end Constructors

        #region Protected Methods

        /// <summary>
        /// Metoda powinna zawierać wszelkie niezbędne operacje do zbudowania stanów potomnych.
        /// </summary>
        /// <param name="parent">Stan rodzica.</param>
        protected abstract void buildChildren(IState parent);

        /// <summary>
        /// Zwraca wartość bool mówiąco czy stan podany w parametrze jest rozwiązaniem.
        /// </summary>
        /// <param name="state">Stan do sprawdzenia.</param>
        /// <returns>Wartość bool czy stan jest rozwiązaniem.</returns>
        protected abstract bool isSolution(IState state);

        #endregion //end Private Methods

        #region Public Methods

        /// <summary>
        /// Wykonanie algorytmu BSF.
        /// </summary>
        public void DoSearch() {
            IState currentState = this.initialState;
            while (true) {
                if (isSolution(currentState)) {
                    solutions.Add(currentState);

                    if (solutions.Count >= numberOfSolutionsToFind) {
                        break;
                    }
                }
                else {
                    buildChildren(currentState);

                    foreach (IState child in currentState.Children) {
                        if (!this.closed.ContainsKey(child.ID) && !this.open.Contains(child)) {
                            this.open.Insert(child);
                        }
                    }
                }
				
                this.closed.Add(currentState.ID, currentState);

				if (this.open.Count == 0) {
                    break;
                }
                else {
                    currentState = this.open.RemoveRoot();
                }
            }
        }

        #endregion //end Public Methods
    }
}