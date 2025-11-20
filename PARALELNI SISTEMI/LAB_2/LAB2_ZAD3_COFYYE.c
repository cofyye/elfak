#include <stdio.h>
#include <mpi.h>

constexpr auto K = 3;

int main(int argc, char** argv) {
	int rank = -1, P = -1, root = 0, broj = 0,
		local_C[K] = {}, local_col[K] = {},
		A[K][K] = {}, B[K][K] = {}, C[K][K] = {};

	MPI_Init(&argc, &argv);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_Comm_size(MPI_COMM_WORLD, &P);
	MPI_Datatype columnType{}, columnTypeEx{};

	if (K != P) {
		return -1;
	}

	if (rank == root) {
		for (int i = 0; i < K; i++) {
			for (int j = 0; j < K; j++) {
				A[i][j] = broj;
				B[i][j] = ++broj;
			}
		}
	}

	MPI_Bcast(B, K * K, MPI_INT, root, MPI_COMM_WORLD);
	MPI_Type_vector(K, 1, K, MPI_INT, &columnType);
	MPI_Type_commit(&columnType);
	MPI_Type_create_resized(columnType, 0, 1 * sizeof(int), &columnTypeEx);
	MPI_Type_commit(&columnTypeEx);
	MPI_Scatter(A, 1, columnTypeEx, local_col, K, MPI_INT, root, MPI_COMM_WORLD);

	for (int i = 0; i < K; i++) {
		for (int j = 0; j < K; j++) {
			printf("process %d local_col = %d\n", rank, local_col[i]);
			local_C[i] += local_col[j] * B[i][j];
		}
	}

	MPI_Gather(local_C, K, MPI_INT, C, 1, columnTypeEx, P - 1, MPI_COMM_WORLD);

	if (rank == P - 1) {
		for (int i = 0; i < K; i++) {
			for (int j = 0; j < K; j++) {
				printf("%d ", C[i][j]);
			}
			printf("\n");
		}
	}

	MPI_Finalize();

	return 0;
}