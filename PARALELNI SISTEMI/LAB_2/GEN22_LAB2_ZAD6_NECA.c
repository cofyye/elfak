//https://github.com/sssteeefaaan/VIII-Semestar/blob/main/Paralelni%20Sistemi/Lab/Laboratorijske%20Vežbe%202022/Lab%203.pdf

#include<stdio.h>
#include<mpi.h>
#define N 3

int main(int argc, char** argv) {
	int rank, P; // P = N
	int i, j , A[N][N], B[N][N], C[N][N], local_A[N],  broj = 0 , local_C[N] , local_max[N];
	MPI_Status status;
	MPI_Init(&argc, &argv);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_Comm_size(MPI_COMM_WORLD, &P);
	if (N != P)
		return -1;
	if (rank == 0) {
		for ( i = 0; i < N; i++)
		{
			for (j = 0; j < N; j++)
			{
				A[i][j] = broj;
				B[i][j] = 1 + broj++;
			}
		}
	}
	MPI_Bcast(B, N * N, MPI_INT, 0, MPI_COMM_WORLD);
	MPI_Scatter(A, N, MPI_INT, local_A, N, MPI_INT, 0, MPI_COMM_WORLD);
	for ( i = 0; i < N; i++)
	{
		local_C[i] = 0;
		for ( j = 0; j < N; j++)
		{
			local_C[i] += local_A[j] * B[j][i];
		}
	}
	MPI_Gather(local_C, N, MPI_INT, C, N, MPI_INT, 0, MPI_COMM_WORLD);
	MPI_Reduce(local_C, local_max, N, MPI_INT, MPI_MAX, 0, MPI_COMM_WORLD);
	if (rank == 0) {
		for ( i = 0; i < N; i++)
		{
			printf("Maksmalni element kolone: %d\n", local_max[i]);
		}
	}

	MPI_Finalize();
	return 0;
}