#include<stdio.h>
#include <stdlib.h>
#include<mpi.h>
#define k 6

int main(int argc, char** argv) {
	struct globalmin {
		int min;
		int rank;
	} globalmin , min;
	int rank, m;
	int i, j , root = 2;
	int A[k][k], niz[k];
	MPI_Init(&argc, &argv);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_Comm_size(MPI_COMM_WORLD, &m);
	MPI_Datatype noviTip , noviTipEx;
	if (k % m != 0 || m <= 2)
		return -1;
	if (rank == root) {
		for (i = 0; i < k; i++)
		{
			for (j = 0; j < k; j++)
			{
				A[i][j] = rand() % 100 + 1;
				printf("%d ", A[i][j]);
			}
			printf("\n");
		}
	}
	MPI_Type_vector(k / 2, k / m, 2 * k, MPI_INT, &noviTip);
	MPI_Type_commit(&noviTip);
	MPI_Type_create_resized(noviTip, 0, k / m * sizeof(int), &noviTipEx);
	MPI_Type_commit(&noviTipEx);
	MPI_Scatter(&(A[1][0]), 1, noviTipEx, niz, k / m * k / 2, MPI_INT, root, MPI_COMM_WORLD);
	min.min = niz[0];
	min.rank = rank;
	for (i = 1; i < k / m * k / 2; i++) {
		if (min.min > niz[i])
			min.min = niz[i];
	}
	MPI_Reduce(&min, &globalmin, 1, MPI_2INT, MPI_MINLOC, root, MPI_COMM_WORLD);
	if (rank == root) {
		printf("globalmin:%d\n", globalmin.min);
		printf("globalmin_rank:%d\n", globalmin.rank);
	}
	MPI_Finalize();
	return 0;
}