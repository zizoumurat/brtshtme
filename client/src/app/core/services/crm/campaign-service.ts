import { InjectionToken } from '@angular/core';
import { CampaignModel } from '@/core/models/crm/campaign.model';
import { ICrudService   } from '../admin/crud-service';

export interface ICampaignService extends ICrudService<CampaignModel> {}

export const CAMPAIGN_SERVICE = new InjectionToken<ICampaignService>('CampaignService');
