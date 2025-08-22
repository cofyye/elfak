import { DynamicModule, Module, Provider } from '@nestjs/common';
import Stripe from 'stripe';
import { StripeApiService } from './stripe-api.service';

@Module({})
export class StripeApiModule {
  static forRoot(apiKey: string, config: Stripe.StripeConfig): DynamicModule {
    const stripe = new Stripe(apiKey, config);

    const stripeProvider: Provider = {
      provide: 'STRIPE_CLIENT',
      useValue: stripe,
    };

    return {
      module: StripeApiModule,
      providers: [stripeProvider, StripeApiService],
      exports: [stripeProvider, StripeApiService],
      global: true,
    };
  }
}
