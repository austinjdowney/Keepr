import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfilesService {
  async getProfile(profileId) {
    try {
      const res = await api.get('/api/profiles/' + profileId)
      AppState.profile = res.data
    } catch (err) {
      logger.error('Something went wrong', err)
    }
  }

  async getKeepsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    AppState.keeps = res.data
  }

  async getVaultsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    AppState.vaults = res.data
  }
}

export const profilesService = new ProfilesService()
