#include <stdio.h>
#include <mpi.h>

constexpr auto N = 4;

int main(int argc, char** argv) {
	int rank = -1, P = -1, root = 0, broj = 0,
		local_A[N] = {}, local_C[N] = {}, local_max[N] = {},
		A[N][N] = {}, B[N][N] = {}, C[N][N] = {};

	MPI_Init(&argc, &argv);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_Comm_size(MPI_COMM_WORLD, &P);
	
	if (N != P) {
		return -1;
	}

	if (rank == root) {
		for (int i = 0; i < N; i++) {
			for (int j = 0; j < N; j++) {
				A[i][j] = broj;
				B[i][j] = ++broj;
			}
		}
	}

	MPI_Bcast(B, N * N, MPI_INT, root, MPI_COMM_WORLD);
	MPI_Scatter(A, N, MPI_INT, local_A, N, MPI_INT, root, MPI_COMM_WORLD);

	for (int i = 0; i < N; i++) {
		for (int j = 0; j < N; j++) {
			local_C[i] += local_A[j] * B[j][i];
		}
	}

	MPI_Reduce(local_C, local_max, N, MPI_INT, MPI_MAX, root, MPI_COMM_WORLD);
	MPI_Gather(local_C, N, MPI_INT, C, N, MPI_INT, root, MPI_COMM_WORLD);

	if (rank == root) {
		for (int i = 0; i < N; i++) {
			for (int j = 0; j < N; j++) {
				printf("%d ", C[i][j]);
			}
			printf("\n");
		}
		printf("\n");
		for (int i = 0; i < N; i++) {
			printf("process %d local_max %d\n", i, local_max[i]);
		}
	}

	MPI_Finalize();

	return 0;
}