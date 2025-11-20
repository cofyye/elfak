#include<stdio.h>
#include<mpi.h>
#define k 3

int main(int argc, char** argv) {
	int rank, size;
	int A[k][k], B[k][k], kolona_A[k], local_c[k], C[k][k];
	int i, j;
	int broj = 0;
	MPI_Datatype temp_type;
	MPI_Datatype kolona;
	MPI_Init(&argc, &argv);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	if (size != k)
		return -1;
	if (rank == 0) {
		for (i = 0; i < k; i++) {
			for (j = 0; j < k; j++) {
				A[i][j] = broj;
				B[i][j] = broj++;
			}
		}
	}
	MPI_Type_vector(k, 1, k, MPI_INT, &temp_type);
	MPI_Type_commit(&temp_type);
	MPI_Type_create_resized(temp_type, 0, 1 * sizeof(int), &kolona);
	MPI_Type_commit(&kolona);
	MPI_Scatter(A, 1, kolona, kolona_A, k, MPI_INT, 0, MPI_COMM_WORLD);
	MPI_Bcast(B, k * k, MPI_INT, 0, MPI_COMM_WORLD);

	// Ispisujemo matricu B
	if (rank == 0) {
		printf("Matrica B nakon MPI_Bcast:\n");
		for (i = 0; i < k; i++) {
			for (j = 0; j < k; j++) {
				printf("%d ", B[i][j]);
			}
			printf("\n");
		}
	}

	for (i = 0; i < k; i++) {
		local_c[i] = 0;
		for (j = 0; j < k; j++) {
			local_c[i] += kolona_A[j] * B[i][j];
		}
	}
	MPI_Gather(local_c, k, MPI_INT, C, 1, kolona, size - 1, MPI_COMM_WORLD);
	if (rank == size - 1) {
		for (i = 0; i < k; i++) {
			for (j = 0; j < k; j++) {
				printf("%d ", C[i][j]);
			}
			printf("\n");
		}
	}
	MPI_Finalize();
	return 0;
}